namespace Covid_App.Entities;

public class ExportJsonData
{
    public string Data { get; set; }
    public int Liczba_zgonow { get; set; }
}

public class ExportBlikPayments
{
    public string Kwartal { get; set; }
    public int Liczba { get; set; }
}

public class ExportFluData
{
    public string Rok { get; set; }
    public int Liczba { get; set; }
}