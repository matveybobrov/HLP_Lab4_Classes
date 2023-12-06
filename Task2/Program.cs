using System;
using System.Threading;

public class Node
{
    public object value;
    public Node next;

    public Node(object item, Node next)
    {
        this.value = item;
        this.next = next;
    }
}

public class Queue
{
    public int length;
    public Node head;
    public Node tail;

    public Queue()
    {
        this.length = 0;
        this.head = this.tail = new Node(null, null);
    }

    public string Push(object item)
    {
        this.length++;
        Node node = new Node(item, null);
        if (this.tail.value == null)
        {
            this.head = this.tail = node;
        }
        else
        {
            this.tail.next = node;
            this.tail = node;
        }
        return "ok";
    }

    public object pop()
    {
        if (this.head.value == null) return null;

        this.length--;
        Node head = this.head;
        if (this.length == 0)
        {
            this.head = this.tail = new Node(null, null);
        }
        else
        {
            this.head = this.head.next;
        }
        return head.value;
    }

    public int Size()
    {
        return this.length;
    }

    public object front()
    {
        return this.head.value;
    }

    public string clear()
    {
        this.head.next = null;
        this.head = this.tail = new Node(null, null);
        this.length = 0;
        return "ok";
    }
}
public class Pizzeria
{
    private static int TOTAL_BAKERS = 2;
    private int freeBakers = TOTAL_BAKERS;

    private static int TOTAL_COURIERS = 3;
    private int freeCouriers = TOTAL_COURIERS;

    private static int TOTAL_STORAGE = 20;
    private int pizzasInStorage = 0;

    private Queue orders = new Queue();
    public Pizzeria()
    {
        this.Run();
    }

    public void HandleOrders()
    {
        if (this.orders.Size() == 0) return;
        /*
        if (this.AnyPizzaInStorage())
        {
            Console.WriteLine("Pizza in storage");
            if (this.AnyFreeCouriers())
            {
                this.DeliverPizza();
            }
            else
            {
                Console.WriteLine("No free couriers");
                //wait for deliver
            }
        }
        else
        {
            Console.WriteLine("No pizza in storage");
            if (this.AnyFreeBakers())
            {
                this.BakePizza();
            }
            else
            {
                Console.WriteLine("No free bakers");
                //wait for baker
            }
        }
        */
    }

    private void BakePizza()
    {
        this.freeBakers--;
        Console.WriteLine("Pizza baked!");
        if (this.AnyFreeStorageSpace())
        {
            this.pizzasInStorage++;
            this.freeBakers++;
        } else
        {
            Console.WriteLine("No free storage space!");
        }
    }

    private void DeliverPizza()
    {
        this.freeCouriers--;
        Console.WriteLine("Pizza delivered!");
        this.freeCouriers++;
    }

    private bool AnyFreeCouriers()
    {
        if (this.freeCouriers <= 0)
        {
            return false;
        }
        return true;
    }

    private bool AnyFreeBakers()
    {
        if (this.freeBakers <= 0)
        {
            return false;
        }
        return true;
    }

    private bool AnyFreeStorageSpace()
    {
        if (this.pizzasInStorage >= TOTAL_STORAGE)
        {
            return false;
        }
        return true;
    }

    private bool AnyPizzaInStorage()
    {
        if (this.pizzasInStorage > 0)
        {
            return true;
        }
        return false;
    }

    public void TryToOrder()
    {
        Random rnd = new Random();
        int orderChance = rnd.Next(0, 100);
        if (orderChance > 90)
        {
            this.orders.Push(1);
            Console.WriteLine("Order placed!");
        }
    }

    public void Run()
    {
        int hours = 8;
        int minutes = hours * 60;
        for (int i = 0; i < 48; i++)
        {
            this.PrintCurrentTime(minutes);
            minutes += 10;

            this.TryToOrder();
            this.PrintMainInfo();
            this.PrintOrdersInfo();
            //this.HandleOrders();

            Thread.Sleep(200); 

        }
        Console.WriteLine("16:00");
        Console.WriteLine("Пиццерия закончила работу!");
    }

    private void PrintCurrentTime(int minutes)
    {
        string hoursToPrint = Convert.ToString(minutes / 60);
        string minutesToPrint = Convert.ToString(minutes % 60);
        if (minutesToPrint == "0")
        {
            minutesToPrint = "00";
        }
        Console.WriteLine($"{hoursToPrint}:{minutesToPrint}");
    }

    private void PrintOrdersInfo()
    {
        for (int i = 0; i < this.orders.Size(); i++)
        {
            Console.WriteLine($"Заказ {i}: в процессе...");
        }
    }

    private void PrintMainInfo()
    {
        Console.WriteLine($"Пицц на складе: {pizzasInStorage}/{TOTAL_STORAGE}");
        Console.WriteLine($"Свободных курьеров: {freeCouriers}/{TOTAL_COURIERS}");
        Console.WriteLine($"Свободных пекарей: {freeBakers}/{TOTAL_BAKERS}");
    }
}

public class Program
{
    public static void Main()
    {
        Pizzeria pizzeria = new Pizzeria();
    }
}