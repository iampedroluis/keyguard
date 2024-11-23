using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;

class Kewgard
{
    private static Dictionary<string, List<(string Email, string Password)>> passwords = new Dictionary<string, List<(string, string)>>();
    private const string masterPassword = "31416369";
    private const string filePath = "passwords.txt";

    private static readonly byte[] encryptionKey = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
    private static readonly byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

    static void Main()
    {
        string run;




        do
        {
            Console.Write("");
            run = Console.ReadLine()?.Trim();

        } while (run != "start");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"                                                                                                  
                                                                                                  
`7MMF' `YMM' `7MM""""""YMM `YMM'   `MM' .g8""""""bgd `7MMF'   `7MF'    db      `7MM""""""Mq.  `7MM""""""Yb.   
  MM   .M'     MM    `7   VMA   ,V .dP'     `M   MM       M     ;MM:       MM   `MM.   MM    `Yb. 
  MM .d""       MM   d      VMA ,V  dM'       `   MM       M    ,V^MM.      MM   ,M9    MM     `Mb 
  MMMMM.       MMmmMM       VMMP   MM            MM       M   ,M  `MM      MMmmdM9     MM      MM 
  MM  VMA      MM   Y  ,     MM    MM.    `7MMF' MM       M   AbmmmqMA     MM  YM.     MM     ,MP 
  MM   `MM.    MM     ,M     MM    `Mb.     MM   YM.     ,M  A'     VML    MM   `Mb.   MM    ,dP' 
.JMML.   MMb..JMMmmmmMMM   .JMML.    `""bmmmdPY    `bmmmmd""'.AMA.   .AMMA..JMML. .JMM..JMMmmmdP'   
                                                                                      
                                                                                                    
                    
                                                Zervetrus.inc


");

        Console.ResetColor();


        // Solicitar la contraseña maestra al inicio
        while (true)
        {
            Console.WriteLine("Who are you?");
            Console.WriteLine();
            string inputPassword = ReadPassword();

            if (inputPassword == masterPassword)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ACCESS GRANTED");
                Console.ResetColor();
                ShowProgressBar(30, 50);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data loads successfully");
                Console.ResetColor();
                LoadPasswords();
                Console.WriteLine();
                break;
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ACCESS DENIED.");
                ShowProgressBar(20, 30);
                Console.WriteLine("DESTROYED DATA. :D Good Luck the nextime");
                Console.WriteLine(@"⠀⠀⠀⠀⠀⠀ㅤㅤ
███████████████████████████
███████▀▀▀░░░░░░░▀▀▀███████
████▀░░░░░░░░░░░░░░░░░▀████
███│░░░░░░░░░░░░░░░░░░░│███
██▌│░░░░░░░░░░░░░░░░░░░│▐██
██░└┐░░░░░░░░░░░░░░░░░┌┘░██
██░░└┐░░░░░░░░░░░░░░░┌┘░░██
██░░┌┘▄▄▄▄▄░░░░░▄▄▄▄▄└┐░░██
██▌░│██████▌░░░▐██████│░▐██
███░│▐███▀▀░░▄░░▀▀███▌│░███
██▀─┘░░░░░░░▐█▌░░░░░░░└─▀██
██▄░░░▄▄▄▓░░▀█▀░░▓▄▄▄░░░▄██
████▄─┘██▌░░░░░░░▐██└─▄████
█████░░▐█─┬┬┬┬┬┬┬─█▌░░█████
████▌░░░▀┬┼┼┼┼┼┼┼┬▀░░░▐████
█████▄░░░└┴┴┴┴┴┴┴┘░░░▄█████
███████▄░░░░░░░░░░░▄███████
██████████▄▄▄▄▄▄▄██████████
███████████████████████████        
⠀⠀⠀                      
  ______________________  
| ZEVETRUS SAYS FUCK YOU! |
 ----------------------
                                            

                                        

⠀⠀⠀⠀⠀⠀⠀⠀⠀");
                Console.ResetColor();
            }
        }

        // Menú principal
        string help;
        do
        {
            help = Console.ReadLine();

        } while (help != "-help");

        while (true)

        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Using: ");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("      command [option]");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Avilable commands:: ");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("      passwd");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n Options:");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("     add                  add Password");
            Console.WriteLine("     -ls                  Show Password");
            Console.WriteLine("     --nano               Edit Password");
            Console.WriteLine("     -rm -rf              Delete Password");
            Console.WriteLine("     --save               Save Passwords");
            Console.WriteLine("     exit                 Exit and Save");
            Console.ResetColor();
            Console.WriteLine();
            string option = Console.ReadLine();

            switch (option)
            {
                case "passwd add":
                    AddPassword();
                    break;
                case "passwd -ls":
                    ViewPassword();
                    break;
                case "passwd --nano":
                    EditPassword();
                    break;
                case "passwd -rm -rf":
                    DeletePassword();
                    break;
                case "passwd --save":
                    SavePasswordsToFile();
                    Console.WriteLine("Passwords saved successfully.");
                    break;
                case "exit":
                    SavePasswordsToFile();
                    Console.WriteLine("finish");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine();
                    break;
            }
        }
    }

    static string ReadPassword()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password.Length--;
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("·");
                password.Append(keyInfo.KeyChar);
            }
        } while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }

    static void AddPassword()
    {
        Console.Write("Site: ");
        string site = Console.ReadLine();

        Console.Write("Username or Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (!passwords.ContainsKey(site))
        {
            passwords[site] = new List<(string, string)>();
        }
        passwords[site].Add((email, password));

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Credentials saved.");
        Console.ResetColor();
    }

    static void ViewPassword()
    {
        if (passwords.Count == 0)
        {
            Console.WriteLine("No passwords stored.");
            return;
        }

        Console.WriteLine("Available sites:");
        int index = 1;
        var sites = new List<string>(passwords.Keys);

        foreach (var site in sites)
        {
            Console.WriteLine($"[{index++}] {site}");
        }

        Console.Write("Select site number: ");
        if (int.TryParse(Console.ReadLine(), out int siteIndex) && siteIndex > 0 && siteIndex <= sites.Count)
        {
            string selectedSite = sites[siteIndex - 1];
            var credentials = passwords[selectedSite];

            Console.WriteLine($"{selectedSite} credentials:");
            Console.WriteLine("+------------------------------+----------------------------+");
            Console.WriteLine("|     Email/Username           |         Password           |");
            Console.WriteLine("+------------------------------+----------------------------+");
            for (int i = 0; i < credentials.Count; i++)
            {
                // Cambiar el color para el índice (opcional)
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"[{i + 1}]");
                Console.ResetColor();

                // Imprimir los valores de forma alineada
                Console.WriteLine($" | {credentials[i].Email,-18}       |    {credentials[i].Password,-18}     |");

                Console.WriteLine("+----------------------------+------------------------------+");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    static void EditPassword()
    {
        if (passwords.Count == 0)
        {
            Console.WriteLine("No passwords stored.");
            return;
        }

        Console.WriteLine("Available sites:");
        int index = 1;
        var sites = new List<string>(passwords.Keys);

        foreach (var site in sites)
        {
            Console.WriteLine($"[{index++}] {site}");
        }

        Console.Write("Select site number: ");
        if (int.TryParse(Console.ReadLine(), out int siteIndex) && siteIndex > 0 && siteIndex <= sites.Count)
        {
            string selectedSite = sites[siteIndex - 1];
            var credentials = passwords[selectedSite];

            Console.WriteLine($"\n{selectedSite} credentials:");
            for (int i = 0; i < credentials.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Email/Username: {credentials[i].Email}, Password: {credentials[i].Password}");
            }

            Console.Write("Select credential number to edit: ");
            if (int.TryParse(Console.ReadLine(), out int credentialIndex) && credentialIndex > 0 && credentialIndex <= credentials.Count)
            {
                Console.WriteLine("1. Edit email");
                Console.WriteLine("2. Edit password");
                string editOption = Console.ReadLine();

                switch (editOption)
                {
                    case "1":
                        Console.Write("New email: ");
                        string newEmail = Console.ReadLine();
                        credentials[credentialIndex - 1] = (newEmail, credentials[credentialIndex - 1].Password);
                        Console.WriteLine("Email updated successfully.");
                        break;
                    case "2":
                        Console.Write("New password: ");
                        string newPassword = Console.ReadLine();
                        credentials[credentialIndex - 1] = (credentials[credentialIndex - 1].Email, newPassword);
                        Console.WriteLine("Password updated successfully.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid credential selection.");
            }
        }
        else
        {
            Console.WriteLine("Invalid site selection.");
        }
    }

    static void DeletePassword()
    {
        if (passwords.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No passwords stored.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Available sites:");
        int index = 1;
        var sites = new List<string>(passwords.Keys);

        foreach (var site in sites)
        {
            Console.WriteLine($"[{index++}] {site}");
        }

        Console.Write("Select site number: ");
        if (int.TryParse(Console.ReadLine(), out int siteIndex) && siteIndex > 0 && siteIndex <= sites.Count)
        {
            string selectedSite = sites[siteIndex - 1];
            var credentials = passwords[selectedSite];

            Console.WriteLine($"\n{selectedSite} credentials:");
            for (int i = 0; i < credentials.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Email/Username: {credentials[i].Email}, Password: {credentials[i].Password}");
            }

            Console.Write("Select credential number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int credentialIndex) && credentialIndex > 0 && credentialIndex <= credentials.Count)
            {
                credentials.RemoveAt(credentialIndex - 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Credential deleted successfully.");
                Console.ResetColor();

                // If no credentials remain for the site, remove the site from the dictionary
                if (credentials.Count == 0)
                {
                    passwords.Remove(selectedSite);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"No more credentials for {selectedSite}. Site removed.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Invalid credential selection.");
            }
        }
        else
        {
            Console.WriteLine("Invalid site selection.");
        }
    }

    static void LoadPasswords()
    {
        if (File.Exists(filePath))
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes(filePath);
                string decryptedData = Decrypt(encryptedData);

                foreach (var line in decryptedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        string site = parts[0];
                        string email = parts[1];
                        string password = parts[2];

                        if (!passwords.ContainsKey(site))
                        {
                            passwords[site] = new List<(string, string)>();
                        }

                        passwords[site].Add((email, password));
                    }
                }
                Console.WriteLine("");
            }
            catch
            {
                Console.WriteLine("Error loading passwords.");
            }
        }
        else
        {
            Console.WriteLine("No password file found. Starting fresh.");
        }
    }

    static void SavePasswordsToFile()
    {
        try
        {
            StringBuilder data = new StringBuilder();
            foreach (var pair in passwords)
            {
                foreach (var credential in pair.Value)
                {
                    data.AppendLine($"{pair.Key}|{credential.Email}|{credential.Password}");
                }
            }
            Console.WriteLine("Saving the following data:");


            byte[] encryptedData = Encrypt(data.ToString());
            File.WriteAllBytes(filePath, encryptedData);
            Console.WriteLine("Passwords saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving passwords: {ex.Message}");
        }
    }

    static byte[] Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (StreamWriter writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                }
                return ms.ToArray();
            }
        }
    }

    static string Decrypt(byte[] cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream(cipherText))
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
    static void ShowProgressBar(int totalSteps, int delay)
    {
        Console.Write("Loading: [");

        for (int i = 0; i <= totalSteps; i++)
        {
            Console.Write("#");
            Thread.Sleep(delay);
        }

        Console.WriteLine("]");
    }

}
