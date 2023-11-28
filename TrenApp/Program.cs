namespace TrenApp
{
    internal class Program
    {
        //Bir tren bileti otomasyonu yapilacak. Program calistiginda kullanicidan guzergah secimi, yolcu bilgileri ve bilet islemleri icin girdiler alinacak. Kullanici guzergah ekleme, bilet satisi, bilet iptali ve rapor seklinde seceneklerden birini secerek islemi gerceklestirecek. Rota ekleme isleminde kullanicidan rota adi istenecek ve temel fiyat girilecek. Bilet satis isleminde kuullanici adi alinacak rota adi yazilacak yolcunun yasi alinacak. Ve bu bilgilerle bilet satisi gerceklesecek.
        //Her biletin detayini tutan bir Ticket class olacak. Bu class ta ayni zamanda bir fiyat hesaplayan metot olacak. Bu metot yas ve temel fiyati baz(parametre) alarak hesap yapacak. Eger ki 18 yasindan kucuk ise %50 indirim olacak.
        //Guzergah detaylarini tutan bir Route class imiz olacak. Burada guzergah adi ve basePrice temel fiyati tutulacak.
        //Ucuncu class son class olarak da bilet islemlerini yonetecek olan TicketSystem Class iniz olacak bu classta biletler ve guzergahlar tutulacak. Rota ekleme Bilet satisi bilet iptali ve rapor hazirlama bu classta metotlar halinde tutulacak.
        static void Main(string[] args)
        {

            List<Route> routeList = new List<Route>() { new Route("Ankara", 100), new Route("Istanbul", 150), new Route("Izmir", 200) };
            List<Ticket> ticketList = new List<Ticket>();

            bool menuDongusu = true;

            Console.WriteLine("1. Bilet Satisi");
            Console.WriteLine("2. Bilet Iptali");
            Console.WriteLine("3. Rapor");
            Console.WriteLine("4. Cikis");
            while (menuDongusu)
            {
                Console.Write("Menuden Secimi Yapiniz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {

                    case "1":
                        Console.Write("Isim Giriniz: ");
                        string customerName = Console.ReadLine();

                        Console.WriteLine("Rotalar");
                        foreach (var route in routeList)
                        {
                            Console.WriteLine($"{route.RouteName} - {route.BasePrice} TL");
                        }
                        Console.Write("Rota Adini giriniz: ");
                        string routeName = Console.ReadLine();

                        Console.Write("Yasinizi Giriniz: ");
                        int age = int.Parse(Console.ReadLine());

                        foreach (var route in routeList)
                        {
                            if (routeName == route.RouteName)
                            {
                                Console.WriteLine("Rota Bulundu");
                                Ticket ticket = new Ticket(customerName, routeName, age);
                                ticket.BasePriceCalculation(route.BasePrice);
                                ticketList.Add(ticket);
                                Console.WriteLine("Satis Basarili");
                            }
                        }
                        break;

                    case "2":
                        if (ticketList.Count > 0)
                        {
                            foreach (var ticket in ticketList)
                            {
                                Console.WriteLine($"Biletiniz \nIsim: {ticket.CustomerName} \nGuzergah: {ticket.RouteName} \nUcret: {ticket.Price}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
                        }
                        Console.WriteLine("Bileti iptal edilecek kullanicinin adini giriniz: ");
                        string ticketOwnerName = Console.ReadLine();
                        if (ticketList.Count > 0)
                        {
                            foreach (var ticket in ticketList.ToList())
                            {
                                if (ticket.CustomerName == ticketOwnerName)
                                {
                                    ticketList.Remove(ticket);
                                    Console.WriteLine("Bilet iptal edildi.");                                    
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
                        }
                        break;

                    case "3":
                        if(ticketList.Count > 0)
                        {
                            double totalPrice = 0;
                            foreach (var ticket in ticketList)
                            {
                                Console.WriteLine($"Biletiniz \nIsim: {ticket.CustomerName} \nGuzergah: {ticket.RouteName} \nUcret: {ticket.Price}");
                                totalPrice += ticket.Price;
                            }
                            Console.WriteLine($"Toplam Fiyat: {totalPrice} TL");
                        }
                        else
                        {
                            Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Program Kapatiliyor...");
                        menuDongusu = false;
                        break;
                    default:
                        Console.WriteLine("Lutfen 1-4 arasi secim yapin");
                        break;
                }
            }
        }
    }
   

    class Ticket
    {
        public string CustomerName { get; set; }
        public string RouteName { get; set; }
        public int Age { get; set; }
        public double Price { get; set; }
        public Ticket(string customerName, string routeName, int age)
        {
            CustomerName = customerName;
            RouteName = routeName;
            Age = age;
        }

        public void BasePriceCalculation(double basePrice)
        {
            double discountRate = Age < 18 ? 0.5 : 1.0;
            Price = basePrice * discountRate;
        }
    }


    class Route
    {
        public string RouteName { get; set; }
        public double BasePrice { get; set; }

        public Route(string routeName, double basePrice)
        {
            RouteName = routeName;
            BasePrice = basePrice;
        }
    }
}
