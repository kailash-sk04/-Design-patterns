class Logger {
    private static Logger instance;

    private Logger() { }

    public static Logger Instance {
        get {
            if (instance == null)
                instance = new Logger();
            return instance;
        }
    }

    public void Log(string message) {
        Console.WriteLine("LOG: " + message);
    }
}

class Program {
    static void Main(string[] args) {
        Logger.Instance.Log("App started");
    }
}
