using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratePGPKeys
{
    public partial class PGPKeysGenerator : Form
    {
        static string publicKey = string.Empty;
        static string privateKey = string.Empty;
        static string encryptedFilePath = string.Empty;
        public PGPKeysGenerator()
        {
            InitializeComponent();
        }       

        public static PgpKeyRingGenerator generateKeyRingGenerator(String identity, String password, int yrs)
        {
            long expirySec = yrs * 365 * 24* 60 * 60;

            KeyRingParams keyRingParams = new KeyRingParams();
            keyRingParams.Password = password;
            keyRingParams.Identity = identity;
            keyRingParams.PrivateKeyEncryptionAlgorithm = SymmetricKeyAlgorithmTag.Aes128;
            keyRingParams.SymmetricAlgorithms = new SymmetricKeyAlgorithmTag[] {
            SymmetricKeyAlgorithmTag.Aes256,
            SymmetricKeyAlgorithmTag.Aes192,
            SymmetricKeyAlgorithmTag.Aes128
        };

            keyRingParams.HashAlgorithms = new HashAlgorithmTag[] {
            HashAlgorithmTag.Sha256,
            HashAlgorithmTag.Sha384,
            HashAlgorithmTag.Sha512
        };

            IAsymmetricCipherKeyPairGenerator generator
                = GeneratorUtilities.GetKeyPairGenerator("RSA");
            generator.Init(keyRingParams.RsaParams);

            /* Create the master (signing-only) key. */
            PgpKeyPair masterKeyPair = new PgpKeyPair(
                PublicKeyAlgorithmTag.RsaSign,
                generator.GenerateKeyPair(),
                DateTime.UtcNow);
          
            PgpSignatureSubpacketGenerator masterSubpckGen
                = new PgpSignatureSubpacketGenerator();
            masterSubpckGen.SetKeyFlags(false, PgpKeyFlags.CanCertify); //PgpKeyFlags.CanSign |  uncomment and copy till cansign and pipe to generate signfile

            masterSubpckGen.SetPreferredSymmetricAlgorithms(false,
                (from a in keyRingParams.SymmetricAlgorithms
                 select (int)a).ToArray());
            masterSubpckGen.SetPreferredHashAlgorithms(false,
                (from a in keyRingParams.HashAlgorithms
                 select (int)a).ToArray());

            /* Create a signing and encryption key for daily use. */
            PgpKeyPair encKeyPair = new PgpKeyPair(
                PublicKeyAlgorithmTag.RsaGeneral,
                generator.GenerateKeyPair(),
                DateTime.UtcNow);
            

            PgpSignatureSubpacketGenerator encSubpckGen = new PgpSignatureSubpacketGenerator();
            encSubpckGen.SetKeyFlags(false, PgpKeyFlags.CanEncryptCommunications | PgpKeyFlags.CanEncryptStorage);
            
            encSubpckGen.SetKeyExpirationTime(true, expirySec‬);


            masterSubpckGen.SetPreferredSymmetricAlgorithms(false,
                (from a in keyRingParams.SymmetricAlgorithms
                 select (int)a).ToArray());
            masterSubpckGen.SetPreferredHashAlgorithms(false,
                (from a in keyRingParams.HashAlgorithms
                 select (int)a).ToArray());
            masterSubpckGen.SetKeyExpirationTime(true, expirySec);

            /* Create the key ring. */
            PgpKeyRingGenerator keyRingGen = new PgpKeyRingGenerator(
                PgpSignature.DefaultCertification,
                masterKeyPair,
                keyRingParams.Identity,
                keyRingParams.PrivateKeyEncryptionAlgorithm.Value,
                keyRingParams.GetPassword(),
                true,
                masterSubpckGen.Generate(),
                null,
                new SecureRandom());

            /* Add encryption subkey. */
            keyRingGen.AddSubKey(encKeyPair, encSubpckGen.Generate(), null);

            return keyRingGen;

        }

        private void folderSelector_HelpRequest(object sender, EventArgs e)
        {

        }

       

        
        /**
       * Search a secret key ring collection for a secret key corresponding to
       * keyId if it exists.
       *
       * @param pgpSec a secret key ring collection.
       * @param keyId keyId we want.
       * @param pass passphrase to decrypt secret key with.
       * @return
       */
        private static PgpPrivateKey FindSecretKey(PgpSecretKeyRingBundle pgpSec, long keyId, char[] pass)
        {

            PgpSecretKey pgpSecKey = pgpSec.GetSecretKey(keyId);
            if (pgpSecKey == null)
            {
                return null;
            }

            return pgpSecKey.ExtractPrivateKey(pass);

        }

        private static void DecryptFile(
         string inputFileName,
         string keyFileName,
         char[] passwd,
         string defaultFileName)
        {
            using (Stream input = File.OpenRead(inputFileName),
                   keyIn = File.OpenRead(keyFileName))
            {
                DecryptFile(input, keyIn, passwd, defaultFileName);
            }
        }

        private static void DecryptFile(
            Stream inputStream,
            Stream keyIn,
            char[] passwd,
            string defaultFileName)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            try
            {
                PgpObjectFactory pgpF = new PgpObjectFactory(inputStream);
                PgpEncryptedDataList enc;

                PgpObject o = pgpF.NextPgpObject();
                //
                // the first object might be a PGP marker packet.
                //
                if (o is PgpEncryptedDataList)
                {
                    enc = (PgpEncryptedDataList)o;
                }
                else
                {
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();
                }

                //
                // find the secret key
                //
                PgpPrivateKey sKey = null;
                PgpPublicKeyEncryptedData pbe = null;
                PgpSecretKeyRingBundle pgpSec = new PgpSecretKeyRingBundle(
                    PgpUtilities.GetDecoderStream(keyIn));

                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(pgpSec, pked.KeyId, passwd);

                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                if (sKey == null)
                {
                    throw new ArgumentException("secret key for message not found.");
                }

                Stream clear = pbe.GetDataStream(sKey);

                PgpObjectFactory plainFact = new PgpObjectFactory(clear);

                PgpObject message = plainFact.NextPgpObject();

                if (message is PgpCompressedData)
                {
                    PgpCompressedData cData = (PgpCompressedData)message;
                    PgpObjectFactory pgpFact = new PgpObjectFactory(cData.GetDataStream());

                    message = pgpFact.NextPgpObject();
                }

                if (message is PgpLiteralData)
                {
                    PgpLiteralData ld = (PgpLiteralData)message;

                    //string outFileName = ld.FileName;
                    //if (outFileName.Length == 0)
                    //{
                    //    outFileName = defaultFileName;
                    //}

                    Stream fOut = File.Create(defaultFileName);
                    Stream unc = ld.GetInputStream();
                    Streams.PipeAll(unc, fOut);
                    fOut.Close();
                }
                else if (message is PgpOnePassSignatureList)
                {
                    throw new PgpException("encrypted message contains a signed message - not literal data.");
                }
                else
                {
                    throw new PgpException("message is not a simple encrypted file - type unknown.");
                }

                if (pbe.IsIntegrityProtected())
                {
                    if (!pbe.Verify())
                    {
                        Console.Error.WriteLine("message failed integrity check");
                    }
                    else
                    {
                        Console.Error.WriteLine("message integrity check passed");
                    }
                }
                else
                {
                    Console.Error.WriteLine("no message integrity check");
                }
            }
            catch (PgpException e)
            {
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }
            }
        }

        private void decryptBtn_Click_1(object sender, EventArgs e)
        {
            if (encryptedFilePath.Length > 0)
            {
                string directoryPath = System.IO.Path.GetDirectoryName(fileTxt.Text);
                string decryptFileName = directoryPath + "\\Decrypted_" + System.IO.Path.GetFileName(fileTxt.Text);
                DecryptFile(
                  encryptedFilePath,
                  privateKey,
                  PasswordTxt.Text.ToCharArray(),
                 decryptFileName
              );
                MessageBox.Show("Decrypted file present at "+ decryptFileName);
            }
            else
                MessageBox.Show("Encrypted file should be present first...");
            fileTxt.ReadOnly = false;
            fileTxt.Enabled = true;
            browseBtn2.Enabled = true;
            decryptBtn.Enabled = false;
        }

        private void encryptBtn_Click_1(object sender, EventArgs e)
        {
            PGPEncryptDecrypt pgp = new PGPEncryptDecrypt();
            string directoryPath = string.Empty;

            if (publicKey.Length > 0)
            {
                if (fileTxt.Text.Length > 0)
                    directoryPath = System.IO.Path.GetDirectoryName(fileTxt.Text);
                else
                    MessageBox.Show("Please select the file to encrypt...");
            }
            else
                MessageBox.Show("public key should be present to encrypt...");
            encryptedFilePath = directoryPath + "\\Encrypted_" + System.IO.Path.GetFileName(fileTxt.Text); ;
            pgp.Encrypt(fileTxt.Text, publicKey, encryptedFilePath);
            MessageBox.Show("Encrypted file present at "+encryptedFilePath);
            fileTxt.ReadOnly = true;
            fileTxt.Enabled = false;
            browseBtn2.Enabled = false;
            decryptBtn.Enabled = true;
        }

        private void keyGenerateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IdentityTxt.Text.Length == 0)
                    MessageBox.Show("Please specify the Identity...");
                else if (PasswordTxt.Text.Length == 0)
                    MessageBox.Show("Please specify the Password...");
                else if (KeyNameTxt.Text.Length == 0)
                    MessageBox.Show("Please specify the KeyName to generate...");
                else if (folderPathTxt.Text.Length == 0)
                    MessageBox.Show("Select the folder into the Key should be generated...");
                else
                {
                    publicKey = folderPathTxt.Text + "\\" + KeyNameTxt.Text + "Public.asc";
                    privateKey = folderPathTxt.Text + "\\" + KeyNameTxt.Text + "Private.asc";
                    String Password = PasswordTxt.Text;
                    String Identity = IdentityTxt.Text;
                    int yrs = Convert.ToInt16(expiryYrsCMB.Items[expiryYrsCMB.SelectedIndex]);

                    PgpKeyRingGenerator krgen = generateKeyRingGenerator(Identity, Password,yrs);

                    // Generate public key ring, dump to file.
                    PgpPublicKeyRing pkr = krgen.GeneratePublicKeyRing();

                    BufferedStream pubout = new BufferedStream(new FileStream(publicKey, System.IO.FileMode.Create));
                    pkr.Encode(pubout);
                    pubout.Close();

                    // Generate private key, dump to file.
                    PgpSecretKeyRing skr = krgen.GenerateSecretKeyRing();
                    BufferedStream secout = new BufferedStream(new FileStream(privateKey, System.IO.FileMode.Create));
                    skr.Encode(secout);
                    secout.Close();

                    MessageBox.Show("Keys are generated at the Path:" + folderPathTxt.Text);
                    Encryptor.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderPathTxt.Text = folderSelector.SelectedPath;
            }
        }

        private void browseBtn2_Click(object sender, EventArgs e)
        {
            DialogResult result = fileSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileTxt.Text = fileSelector.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PGPKeysGenerator_Load(object sender, EventArgs e)
        {
            expiryYrsCMB.SelectedIndex = 0;
            decryptBtn.Enabled = false;
        }
    }

    // Define other methods and classes here
    public class PGPEncryptDecrypt
    {

        public PGPEncryptDecrypt()
        {

        }

        /**
        * A simple routine that opens a key ring file and loads the first available key suitable for
        * encryption.
        *
        * @param in
        * @return
        * @m_out
        * @
        */
        private static PgpPublicKey ReadPublicKey(Stream inputStream)
        {

            inputStream = PgpUtilities.GetDecoderStream(inputStream);
            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);
            //
            // we just loop through the collection till we find a key suitable for encryption, in the real
            // world you would probably want to be a bit smarter about this.
            //
            //
            // iterate through the key rings.
            //
            foreach (PgpPublicKeyRing kRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey k in kRing.GetPublicKeys())
                {
                    if (k.IsEncryptionKey)
                    {
                        return k;
                    }

                }
            }

            throw new ArgumentException("Can't find encryption key in key ring.");

        }

       
       
      
        private static void EncryptFile(Stream outputStream, string fileName, PgpPublicKey encKey, bool armor, bool withIntegrityCheck)
        {

            if (armor)
            {

                outputStream = new ArmoredOutputStream(outputStream);

            }

            try
            {

                MemoryStream bOut = new MemoryStream();
                PgpCompressedDataGenerator comData = new PgpCompressedDataGenerator(
                CompressionAlgorithmTag.Zip);
                PgpUtilities.WriteFileToLiteralData(
                comData.Open(bOut),
                PgpLiteralData.Binary,
                new FileInfo(fileName));
                comData.Close();
                PgpEncryptedDataGenerator cPk = new PgpEncryptedDataGenerator(
                SymmetricKeyAlgorithmTag.Cast5, withIntegrityCheck, new SecureRandom());
                cPk.AddMethod(encKey);
                byte[] bytes = bOut.ToArray();
                Stream cOut = cPk.Open(outputStream, bytes.Length);
                cOut.Write(bytes, 0, bytes.Length);
                cOut.Close();
                if (armor)
                {

                    outputStream.Close();

                }


            }

            catch (PgpException e)
            {

                Console.Error.WriteLine(e);
                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {

                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);

                }

            }

        }

        public void Encrypt(string filePath, string publicKeyFile, string pathToSaveFile)
        {

            Stream keyIn, fos;
            keyIn = File.OpenRead(publicKeyFile);
            //string[] fileSplit = filePath.Split('\\');
            //string fileName = fileSplit[fileSplit.Length - 1];
            fos = File.Create(pathToSaveFile);
            EncryptFile(fos, filePath, ReadPublicKey(keyIn), true, true);
            keyIn.Close();
            fos.Close();

        }
      
    }

    class KeyRingParams
    {

        public SymmetricKeyAlgorithmTag? PrivateKeyEncryptionAlgorithm { get; set; }
        public SymmetricKeyAlgorithmTag[] SymmetricAlgorithms { get; set; }
        public HashAlgorithmTag[] HashAlgorithms { get; set; }
        public RsaKeyGenerationParameters RsaParams { get; set; }
        public string Identity { get; set; }
        public string Password { get; set; }
        //= EncryptionAlgorithm.NULL;

        public char[] GetPassword()
        {
            return Password.ToCharArray();
        }

        public KeyRingParams()
        {
            //Org.BouncyCastle.Crypto.Tls.EncryptionAlgorithm
            RsaParams = new RsaKeyGenerationParameters(BigInteger.ValueOf(0x10001), new SecureRandom(), 2048, 12);
        }

    }

}
