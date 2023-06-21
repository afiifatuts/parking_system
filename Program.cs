namespace ParkingSystem;
using System;
using System.Collections.Generic;//provides generic collection like <List>, Dictionary<key,value>
using System.Linq; //provide method to manipulating data like WHERE, SELECT
using System.Text.RegularExpressions; 

enum TypeVehicle
{
    Mobil,
    Motor
}

class Vehicle
{
    public int Slot { get; }
    public string PlatNumber { get; }
    public string Color { get; }
    public TypeVehicle Type { get; }
    public DateTime CheckInTime { get; }

    //constructor
    public Vehicle(int slot, string platNumber, string color, TypeVehicle type, DateTime checkInTime)
    {
        Slot = slot;
        PlatNumber = platNumber;
        Color = color;
        Type = type;
        CheckInTime = checkInTime;
    }
}

class ParkingLot
{
    private Dictionary<int, Vehicle> parkingSlots; //store the vehicles parked in parkingSlots
    private int capacity; //capacity of parking lot

    //create capacity of parkingLot
    public ParkingLot(int capacity)
    {
        this.capacity = capacity;
        parkingSlots = new Dictionary<int, Vehicle>(capacity);
    }
    //IsFull
    private bool IsFull()
    {
        return parkingSlots.Count >= capacity;
    }
    //GetAvailableSlot
    private int GetAvailableSlot()
    {
        for (int slotNumber = 1; slotNumber <= capacity; slotNumber++)
        {
            if (!parkingSlots.ContainsKey(slotNumber)) //check if key in parkingslot not contain slotNumber 
            {
                return slotNumber; // if available slots return slotNumber
            }
        }
        return -1;// if no available slots return -1
    }

    //IsValidSlot
    private bool IsValidSlot(int slotNumber)
    {
        return parkingSlots.ContainsKey(slotNumber);
    }

    //ParkVehicle
    public void ParkVehicle(string platNumber, string color, TypeVehicle type)
    {
        if (IsFull())
        {
            Console.WriteLine("Sorry, parking lot is full");
            return;
        }

        int slotNumber = GetAvailableSlot();
        Vehicle vehicle = new Vehicle(slotNumber, platNumber, color, type, DateTime.Now);
        parkingSlots[slotNumber] = vehicle;
        Console.WriteLine($"Allocated slot number: {slotNumber}");
    }

    //LeaveSlot
    public void LeaveSlot(int slotNumber)
    {
        if (IsValidSlot(slotNumber))
        {
            parkingSlots.Remove(slotNumber);
            Console.WriteLine($"Slot number {slotNumber} is free");
        }
        else
        {
            Console.WriteLine($"Invalid slot number: {slotNumber}");
        }

    }

    //GetStatus
    public void GetStatus()
    {
        Console.WriteLine("Slot\tPlat Number\tType\tColour");
        foreach (var v in parkingSlots)
        {
            int slotNumber = v.Key;
            Vehicle vehicle = v.Value;
            Console.WriteLine($"{slotNumber}\t{vehicle.PlatNumber}\t{vehicle.Type}\t{vehicle.Color}");
        }
    }
    //GetPlatNumberByType
    public List<string> GetPlatNumberByType(TypeVehicle type)
    {
        //cari v yang sama dengan type, kemudian select platnumbernya dan disimpan ke list
        return parkingSlots.Values.Where(v => v.Type == type).Select(v => v.PlatNumber).ToList();
    }

    //GetOddPlateNumbers
    public List<string> GetOddPlateNumbers()
    {
        List<string> oddPlates = new List<string>();

        foreach (var lot in parkingSlots.Values)
        {
            int parseToInt = int.Parse(Regex.Match(lot.PlatNumber, @"\d+").Value);//extract the numeric
            int lastDigit = parseToInt % 10;
            if (lastDigit % 2 != 0)
            {
                oddPlates.Add(lot.PlatNumber);
            }
        }
        return oddPlates;
    }

    //GetEvenPlateNumbers
    public List<string> GetEvenPlateNumbers()
    {
        List<string> evenPlates = new List<string>();

        foreach (var lot in parkingSlots.Values)
        {
            int parseToInt = int.Parse(Regex.Match(lot.PlatNumber, @"\d+").Value);//extract the numeric
            int lastDigit = parseToInt % 10;
            if (lastDigit % 2 == 0)
            {
                evenPlates.Add(lot.PlatNumber);
            }
        }
        return evenPlates;
    }

    //GetPlatNumberByColor
    public List<string> GetPlatNumberByColor(string color)
    {
        List<string> plats = new List<string>();

        foreach (var v in parkingSlots.Values)
        {
            if (string.Equals(v.Color, color, StringComparison.OrdinalIgnoreCase))
            {
                plats.Add(v.PlatNumber);
            }

        }
        return plats;
    }

    //GetSlotsByColor

    public List<int> GetSlotsByColor(string color)

    {
        List<int> slotNumbers = new List<int>();
        foreach (var kvp in parkingSlots)
        {
            if (string.Equals(kvp.Value.Color, color, StringComparison.OrdinalIgnoreCase))
            {
                slotNumbers.Add(kvp.Key);
            }
        }


        return slotNumbers;

    }

    //GetSlotByPlatNumber
    public int GetSlotByPlatNumber(string platNumber)
    {
        var vehicle = parkingSlots.Values.FirstOrDefault(v => v.PlatNumber == platNumber);
        return vehicle != null ? vehicle.Slot : -1;
    }

}

class Program
{
    static ParkingLot parkingLot;
    static void Main()
    {
        Console.WriteLine("===== Parking System Command =====");
        Console.WriteLine("create_parking_lot {capacity}");
        Console.WriteLine("park {platNumber | color | type}");
        Console.WriteLine("leave {slot number}");
        Console.WriteLine("status");
        Console.WriteLine("type_of_vehicles {type_vechicle}");
        Console.WriteLine("registration_numbers_for_vehicles_with_ood_plate");
        Console.WriteLine("registration_numbers_for_vehicles_with_event_plate");
        Console.WriteLine("slot_numbers_for_vehicles_with_colour {color_vechicle} ");
        Console.WriteLine("slot_number_for_registration_number {plat_number}");
        Console.WriteLine("exit");

        while (true)
        {
            Console.Write("Write your command :");
            string input = Console.ReadLine();
            string[] parts = input.Split(" ");
            string command = parts[0];

            switch (command)
            {
                case "create_parking_lot":
                    int capacity = int.Parse(parts[1]);
                    CreateParkingLot(capacity);
                    break;
                case "park":
                    string platNumber = parts[1];
                    string color = parts[2];
                    //string yang akan dikonversi menjadi tipe enumerasi, ignore case, menyimpan nilainya ke type
                    if (Enum.TryParse<TypeVehicle>(parts[3], ignoreCase: true, out TypeVehicle type))
                    {
                        ParkVehicle(platNumber, color, type);
                    }
                    else
                    {
                        Console.WriteLine("Invalid vehicle type.");
                    }
                    break;
                case "leave":
                    int slotNumber = int.Parse(parts[1]);
                    LeaveSlot(slotNumber);
                    break;
                case "status":
                    GetStatus();
                    break;
                case "type_of_vehicles":
                    string vehicleType = parts[1];
                    GetTypeOfVehicles(vehicleType);
                    break;
                case "registration_numbers_for_vehicles_with_ood_plate":
                    GetOddPlate();
                    break;
                case "registration_numbers_for_vehicles_with_event_plate":
                    GetEventPlate();
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    string targetColor1 = parts[1];
                    GetPlatNumByColor(targetColor1);
                    break;
                case "slot_numbers_for_vehicles_with_colour":
                    string targertColor2 = parts[1];
                    GetSlotByColor(targertColor2);
                    break;
                case "slot_number_for_registration_number":
                    string targetPlatNum = parts[1];
                    GetSlotByPlatNum(targetPlatNum);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }

        }

        //Service 
        //CreateParkingLot
        static void CreateParkingLot(int capacity)
        {
            parkingLot = new ParkingLot(capacity);
            Console.WriteLine($"Created a parking lot with {capacity} slots");
        }

        //ParkVehicle
        static void ParkVehicle(string platNumber, string color, TypeVehicle type)
        {
            parkingLot.ParkVehicle(platNumber, color, type);
        }

        //LeaveSlot
        static void LeaveSlot(int slotNumber)
        {
            parkingLot.LeaveSlot(slotNumber);
        }

        //GetStatus
        static void GetStatus()
        {
            parkingLot.GetStatus();
        }

        //GetByTypeOfVehicles
        static void GetTypeOfVehicles(string vehicleType)
        {
            TypeVehicle type = Enum.Parse<TypeVehicle>(vehicleType, ignoreCase: true);
            List<string> platNumbers = parkingLot.GetPlatNumberByType(type);
            Console.WriteLine(platNumbers.Count);
        }

        //GetOddPlate
        static void GetOddPlate()
        {
            List<string> platNumbers = parkingLot.GetOddPlateNumbers();
            string result = string.Join(",", platNumbers);
            if (platNumbers.Count == 0)
            {
                Console.WriteLine("Not found.");
            }
            else
            {
                Console.WriteLine(result);
            }
        }

        //GetEventPlate
        static void GetEventPlate()
        {
            List<string> platNumbers = parkingLot.GetEvenPlateNumbers();
            string result = string.Join(",", platNumbers);
            if (platNumbers.Count == 0)
            {
                Console.WriteLine("Not found.");
            }
            else
            {
                Console.WriteLine(result);
            }
        }

        //GetPlatNumByColor
        static void GetPlatNumByColor(string targetColor1)
        {
            List<string> platNumbers = parkingLot.GetPlatNumberByColor(targetColor1);
            string result = string.Join(",", platNumbers);
            if (platNumbers.Count == 0)
            {
                Console.WriteLine("Not found.");
            }
            else
            {
                Console.WriteLine(result);
            }
        }

        //GetSlotByColor
        static void GetSlotByColor(string color)
        {

            List<int> slots = parkingLot.GetSlotsByColor(color);
            string result = string.Join(", ", slots);
            Console.WriteLine(result);
        }

        //GetSlotByPlatNum
        static void GetSlotByPlatNum(string platNumber)
        {
            int slotNumber = parkingLot.GetSlotByPlatNumber(platNumber);
            if (slotNumber != -1)
            {
                Console.WriteLine(slotNumber);
            }
            else
            {
                Console.WriteLine("Not Found");
            }
        }

    }
}
