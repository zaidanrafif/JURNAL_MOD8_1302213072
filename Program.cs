using System.Runtime.CompilerServices;
using System.Text.Json;

public class BankTransfer
{
    public string lang { get; set; }
    public transfer transfer { get; set; }
    public List<string> methods { get; set; }
    public confirmation confirmation { get; set; }

    public BankTransfer(string lang, transfer transfer, List<string> methods, confirmation confirmation)
    {
        this.lang = lang;
        this.transfer = transfer;
        this.methods = methods;
        this.confirmation = confirmation;
    }

    public BankTransfer() { }
}

public class transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }

    public transfer(int threshold, int low_fee, int high_fee)
    {
        this.threshold = threshold;
        this.low_fee = low_fee;
        this.high_fee = high_fee;
    }

    public transfer() { }
}

public class confirmation
{
    public string en { get; set; }
    public string id { get; set; }

    public confirmation(string en, string id)
    {
        this.en = en;
        this.id = id;
    }

    public confirmation() { }
}


public class BankTransferConfig
{
    public BankTransfer BT;
    private const string fileLoc = ".";
    private const string filepath = fileLoc + "\\" + @"bank_transfer_config.json";
    public transfer TF;
    public confirmation confirm;

    public BankTransferConfig()
    {
        try
        {
            readConfig();
        }
        catch
        {
            setDefault();
            writeBankTransferConfig();
        }
    }

    public void setDefault()
    {
        string CONFIG1 = "en";
        int CONFIG2 = 25000000;
        int CONFIG3 = 6500;
        int CONFIG4 = 15000;
        List<string> CONFIG5 = new List<string> { "RTO (real-time)", "SKN", "RTGS", "BI FAST" };
        string CONFIG6 = "yes";
        string CONFIG7 = "ya";
        confirm = new confirmation(CONFIG6, CONFIG7);
        TF = new transfer(CONFIG2, CONFIG3, CONFIG4);
        BT = new BankTransfer(CONFIG1, TF, CONFIG5, confirm);
    }

    public void writeBankTransferConfig()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        string json = JsonSerializer.Serialize(BT, options);
        File.WriteAllText(filepath, json);
    }

    public BankTransfer readConfig()
    {
        string hasilBaca = File.ReadAllText(filepath);
        BT = JsonSerializer.Deserialize<BankTransfer>(hasilBaca);
        return BT;
    }
}

public class program
{
    private static void Main(string[] args)
    {
        BankTransferConfig bankTransferConfig = new BankTransferConfig();

        if (bankTransferConfig.BT.lang == "en")
        {
            Console.Write("Please insert the amount of money to transfer: ");
            int amountTF = Convert.ToInt32(Console.ReadLine());

            if (amountTF <= bankTransferConfig.BT.transfer.threshold)
            {
                Console.WriteLine("Tranfer Fee = " + bankTransferConfig.BT.transfer.low_fee);
                int FeeTotal = bankTransferConfig.BT.transfer.low_fee + amountTF;
                Console.WriteLine("Total Amount = " + FeeTotal);

                Console.WriteLine("Select transfer method: ");
                for (int i = 0; i < bankTransferConfig.BT.methods.Count; i++)
                {
                    Console.WriteLine((i + 1) + " " + bankTransferConfig.BT.methods[i]);
                }
                string methods = Console.ReadLine();
                Console.WriteLine("Please type " + bankTransferConfig.BT.confirmation.en + " to confirm the transaction");
                string confirmation = Console.ReadLine();
                if (confirmation == bankTransferConfig.BT.confirmation.en)
                {
                    Console.WriteLine("The transfer is completed");
                }
                else
                {
                    Console.WriteLine("Transfer is cancelled");
                }

            }
            else
            {
                Console.WriteLine("Tranfer Fee = " + bankTransferConfig.BT.transfer.high_fee);
                int FeeTotal = bankTransferConfig.BT.transfer.high_fee + amountTF;
                Console.WriteLine("Total Amount: " + FeeTotal);

                Console.WriteLine("Select transfer method: ");
                for (int i = 0; i < bankTransferConfig.BT.methods.Count; i++)
                {
                    Console.WriteLine((i + 1) + " " + bankTransferConfig.BT.methods[i]);
                }
                string methods = Console.ReadLine();
                Console.WriteLine("Please type " + bankTransferConfig.BT.confirmation.en + " to confirm the transaction");
                string confirmation = Console.ReadLine();
                if (confirmation == bankTransferConfig.BT.confirmation.en)
                {
                    Console.WriteLine("The transfer is completed");
                }
                else
                {
                    Console.WriteLine("Transfer is cancelled");
                }
            }

        }
        else if (bankTransferConfig.BT.lang == "id")
        {
            Console.Write("Masukkan jumlah uang yang akan di-transfer: ");
            int amountTF = Convert.ToInt32(Console.ReadLine());

            if (amountTF <= bankTransferConfig.BT.transfer.threshold)
            {
                Console.WriteLine("Biaya Transfer = " + bankTransferConfig.BT.transfer.low_fee);
                int FeeTotal = bankTransferConfig.BT.transfer.low_fee + amountTF;
                Console.WriteLine("Total Biaya = " + FeeTotal);

                Console.WriteLine("Pilih metode transfer: ");
                for (int i = 0; i < bankTransferConfig.BT.methods.Count; i++)
                {
                    Console.WriteLine((i + 1) + " " + bankTransferConfig.BT.methods[i]);
                }
                string methods = Console.ReadLine();
                Console.WriteLine("Ketik " + bankTransferConfig.BT.confirmation.id + " untuk mengkonfirmasi transaksi");
                string confirmation = Console.ReadLine();
                if (confirmation == bankTransferConfig.BT.confirmation.id)
                {
                    Console.WriteLine("Proses transfer berhasil");
                }
                else
                {
                    Console.WriteLine("Transfer dibatalkan");
                }

            }
            else
            {
                Console.WriteLine("Biaya Transfer = " + bankTransferConfig.BT.transfer.high_fee);
                int FeeTotal = bankTransferConfig.BT.transfer.high_fee + amountTF;
                Console.WriteLine("Total Biaya = " + FeeTotal);

                Console.WriteLine("Pilih metode transfer: ");
                for (int i = 0; i < bankTransferConfig.BT.methods.Count; i++)
                {
                    Console.WriteLine((i + 1) + " " + bankTransferConfig.BT.methods[i]);
                }
                string methods = Console.ReadLine();
                Console.WriteLine("Ketik " + bankTransferConfig.BT.confirmation.id + " untuk mengkonfirmasi transaksi");
                string confirmation = Console.ReadLine();
                if (confirmation == bankTransferConfig.BT.confirmation.id)
                {
                    Console.WriteLine("Proses transfer berhasil");
                }
                else
                {
                    Console.WriteLine("Transfer dibatalkan");
                }
            }

        }
    }
}