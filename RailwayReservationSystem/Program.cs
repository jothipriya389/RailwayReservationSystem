// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Linq;

int AvaliableTickets = 63;
int AvaliableUpperBerth = 1;
int AvailableLowerBerth = 1;
int AvailableMiddleBerth = 1;
int AvailableRac = 1;
int WaitingList = 1;
bool loop = true;
List<PassengerList> passengerList = new List<PassengerList>();
int id = 0;


Console.WriteLine("Hi");

while (loop)
{
    Console.WriteLine();
    Console.WriteLine("1.Book Ticket");
    Console.WriteLine("2.Cancel Ticket");
    Console.WriteLine("3.Print All Avaliable Tickets");
    Console.WriteLine("4.Print Booked Tickets");
    Console.WriteLine("5.Exit");

    Console.WriteLine("Enter Your Choice");

    int val = Convert.ToInt32(Console.ReadLine());

    switch (val)
    {
        case 1:
            TicketBooking();
            break;
        case 2:
            CancelTicket();
            break;
        case 3:
            PrintAvailableTickets();
            break;
        case 4:
            PrintPassengerList();
            break;
        default:
            loop = false;
            break;
    };
}


void TicketBooking()
{
    Console.WriteLine("Enter The Passenger Name");
    var Name = Console.ReadLine();
    Console.WriteLine("Enter Your Age");
    var Age = Console.ReadLine();
    Console.WriteLine("Enter Your Birth Preference Like L,M,U");
    var Berth = Console.ReadLine();

    if(Berth?.ToUpper() == "U" || Berth?.ToUpper() == "M" || Berth?.ToUpper() == "L")
    {
        if(Berth?.ToUpper() == "U" && AvaliableUpperBerth > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, Berth, passengerId);
            AvaliableUpperBerth--;
            Console.WriteLine("Upper Berth Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }
        else if (Berth?.ToUpper() == "M" && AvailableMiddleBerth > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, Berth, passengerId);
            AvailableMiddleBerth--;
            Console.WriteLine("Middle Berth Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }
        else if (Berth?.ToUpper() == "L" && AvailableLowerBerth > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, Berth, passengerId);
            AvailableLowerBerth--;
            Console.WriteLine("Lower Berth Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }
        else if(AvaliableUpperBerth>0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, "U", passengerId);
            AvaliableUpperBerth--;
            Console.WriteLine("Upper Berth Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }
        else if(AvailableMiddleBerth > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, "M", passengerId);
            AvailableMiddleBerth--;
            Console.WriteLine("Only Middle Berth Available");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }else if(AvailableLowerBerth> 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, "L", passengerId);
            AvailableLowerBerth--;
            Console.WriteLine("Lower Berth Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
        }
        else if(AvailableRac > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, "RAC", passengerId);
            Console.WriteLine("Rac Ticket Confirmed Successfully");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
            AvailableRac--;
        }else if(WaitingList < 10 && WaitingList > 0)
        {
            int passengerId = id++;
            BookTicket(Name, Age, "WL", passengerId);
            Console.WriteLine("You are on Waiting List");
            Console.WriteLine($"Your Passenger Id : {passengerId}");
            WaitingList--;
        }
        else
        {
            Console.WriteLine("There was no ticket available");
        }
    }
}

void BookTicket(string Name,string Age, string Berth,int id)
{
    PassengerList passenger = new PassengerList
    {
        Name = Name,
        Age = Age,
        Berth = Berth,
        PassengerId = id
    };
    passengerList.Add(passenger);
}

void PrintAvailableTickets()
{
    Console.WriteLine($"Available Upper Berths : {AvaliableUpperBerth}");
    Console.WriteLine($"Available Middle Berths: {AvailableMiddleBerth}");
    Console.WriteLine($"Available Lower Berths: { AvailableLowerBerth}");
    Console.WriteLine($"Available RAC Berths: { AvailableRac}");
    Console.WriteLine($"Available Waiting List: {WaitingList}");
}

void PrintPassengerList()
{
    Console.WriteLine("Printing Passenger List:");
    foreach (var passenger in passengerList)
    {
        Console.WriteLine($"Passenger Id: {passenger.PassengerId}");
        Console.WriteLine($"Name: {passenger.Name}");
        Console.WriteLine($"Age: {passenger.Age}");
        Console.WriteLine($"Berth: {passenger.Berth}");
        Console.WriteLine(); // Add a blank line between each person
    }
}

void CancelTicket()
{
    Console.WriteLine("Enter Your Passenger Id");
    int val = Convert.ToInt32(Console.ReadLine());
    var passenger = passengerList.SingleOrDefault(p => p.PassengerId == val);
    if (passenger != null)
    {
        if(passenger.Berth == "WL")
        {
            passengerList.Remove(passenger);
            WaitingList++;
        }else if(passenger.Berth == "RAC")
        {
            passengerList.Remove(passenger);
            AvailableRac++;
            if(passengerList.Count > 0)
            {
                PassengerList WaitingId = passengerList.FirstOrDefault(p => p.Berth == "WL");
                if (WaitingId != null)
                {
                    WaitingList++;
                    AvailableRac--;
                    WaitingId.Berth = "RAC";
                }
            }
        }
        else
        {
            var BerthName = passenger.Berth;
            if(BerthName == "U")
            {
                AvaliableUpperBerth++;
            }
            else if(BerthName == "M")
            {
                AvailableMiddleBerth++;
            }
            else
            {
                AvailableLowerBerth++;
            }
            passengerList.Remove(passenger);
            if (passengerList.Count > 0)
            {
                PassengerList RACList = passengerList.FirstOrDefault(p => p.Berth == "RAC");
                if (RACList != null)
                {
                    if (BerthName == "U")
                    {
                        AvaliableUpperBerth--;
                    }
                    else if (BerthName == "M")
                    {
                        AvailableMiddleBerth--;
                    }
                    else
                    {
                        AvailableLowerBerth--;
                    }
                    AvailableRac++;
                    RACList.Berth = BerthName;
                }
                PassengerList WaitingId = passengerList.FirstOrDefault(p => p.Berth == "WL");
                if (WaitingId != null)
                {
                    WaitingList++;
                    AvailableRac--;
                    WaitingId.Berth = "RAC";
                }
            }
        }
        Console.WriteLine("Ticket Cancelled Successfully");
    }
    else
    {
        Console.WriteLine("Kindly Enter Correct Passenger Id");
    }
}

public class PassengerList
{
    public string? Name { get;set; }
    public string? Age { get; set; }
    public string? Berth { get; set; }
    public int PassengerId { get; set; }
}


