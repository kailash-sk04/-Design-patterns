class OldPrinter {
    public void Print() {
        Console.WriteLine("Old printer printing...");
    }
}

interface IUSBPrinter {
    void ConnectAndPrint();
}

class PrinterAdapter : IUSBPrinter {
    private OldPrinter oldPrinter;

    public PrinterAdapter(OldPrinter printer) {
        oldPrinter = printer;
    }

    public void ConnectAndPrint() {
        oldPrinter.Print();
    }
}

class Program {
    static void Main(string[] args) {
        IUSBPrinter printer = new PrinterAdapter(new OldPrinter());
        printer.ConnectAndPrint();
    }
}
