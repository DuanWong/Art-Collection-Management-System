using System;
using System.IO;
using Art_Collection_Manager;

public class Program
{
    static Artwork[] artworks = new Artwork[20];
    static int count;

    // Read data from file
    static void ReadData(string fileName)
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4)
                {
                    string title = parts[0];
                    string artist = parts[1];
                    int year = int.Parse(parts[2]);
                    string medium = parts[3];

                    AddArtwork(title, artist, year, medium, false);
                }
            }
        }
    }

    // Save data to file
    static void SaveData(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine($"{artworks[i].Title}|{artworks[i].Artist}|{artworks[i].Year}|{artworks[i].Medium}");
            }
        }
        Console.WriteLine("\nChanges has been saved! Thanks for using the Art Collection Manager!");
    }

    // Display all artworks
    static void DisplayAllArtworks()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Title: {artworks[i].Title}, Artist: {artworks[i].Artist}, Year: {artworks[i].Year}, Medium: {artworks[i].Medium}");
        }
    }

    // Insertion sort by title
    static void InsertionSortByTitle()
    {
        for (int i = 1; i < count; i++)
        {
            Artwork flag = artworks[i];
            int j = i - 1;
            while (j >= 0 && string.Compare(artworks[j].Title, flag.Title) > 0)
            {
                artworks[j + 1] = artworks[j];
                j--;
            }
            artworks[j + 1] = flag;
        }
        Console.WriteLine("\nThe artworks have sorted by title:\n");

        DisplayAllArtworks();
    }

    // Insertion sort by year
    static void InsertionSortByYear()
    {
        for (int i = 1; i < count; i++)
        {
            Artwork flag = artworks[i];
            int j = i - 1;
            while (j >= 0 && artworks[j].Year > flag.Year)
            {
                artworks[j + 1] = artworks[j];
                j--;
            }
            artworks[j + 1] = flag;
        }
        Console.WriteLine("\nThe artworks have sorted by year:\n");

        DisplayAllArtworks();
    }

    // Find an artwork by title - Binary search
    static int BinarySearchByTitle(string target)
    {
        int lo = 0, hi = count - 1;
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            int result = string.Compare(artworks[mid].Title, target);
            if (result == 0)
            {
                return mid;
            }
            else if (result < 0)
            {
                lo = mid + 1;
            }
            else
            {
                hi = mid - 1;
            }
        }
        return -1; 
    }

    // Add new artwork
    static void AddArtwork(string title, string artist, int year, string medium, bool isManual = true)
    {
        if (count >= artworks.Length) 
        {
            Array.Resize(ref artworks, artworks.Length + 10);
        }

        int i = count - 1;
        while (i >= 0 && artworks[i].Year > year)
        {
            artworks[i + 1] = artworks[i];
            i--;
        }

        artworks[i + 1] = new Artwork(title, artist, year, medium);
        count++;

        if (isManual)
        {
            Console.WriteLine("\nNew artwork added successfully!");
        }
    }

    // Delete artwork by title
    static void DeleteArtworkByTitle(string targetTitle)
    {
        int index = BinarySearchByTitle(targetTitle);
        if (index == -1)
        {
            Console.WriteLine("Artwork not found!");
            return;
        }

        for (int i = index; i < count - 1; i++)
        {
            artworks[i] = artworks[i + 1];
        }

        count--;
        artworks[count] = null!; // 清空最后一项引用

        Console.WriteLine("Artwork deleted successfully.");
    }

    static void Main(string[] args)
    {
        string filename = "art_collection_basic.txt";

        ReadData(filename);

        bool working = true;
        while (working)
        {
            Thread.Sleep(1000);
            Console.WriteLine("\n>>>Art Collection Management System<<<");

            Console.WriteLine("\n**View & Search**");
            Console.WriteLine("1. Display all artworks");
            Console.WriteLine("2. Search artwork");

            Console.WriteLine("\n**Manage Artworks**");
            Console.WriteLine("3. Add new artwork");
            Console.WriteLine("4. Delete artwork");
            Console.WriteLine("5. Sort artworks by title");
            Console.WriteLine("6. Sort artworks by year");

            Console.WriteLine("\n**Save & Exit**");
            Console.WriteLine("7. Save and exit");
            Console.WriteLine("8. Exit without saving\n");
            Console.Write("Choose an option: ");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("\nLoading artworks...");
                    Thread.Sleep(2000);
                    DisplayAllArtworks();
                    break;
                case "2":
                    Console.Write("Enter title to search artwork: ");
                    string? searchTitle = Console.ReadLine();
                    int index = BinarySearchByTitle(searchTitle);
                    if (index != -1)
                    {
                        Artwork foundArtwork = artworks[index];
                        Console.WriteLine($"\nSearch result: \nTitle: {foundArtwork.Title}, Artist: {foundArtwork.Artist}, Year: {foundArtwork.Year}, Medium: {foundArtwork.Medium}");
                    }
                    else
                    {
                        Console.WriteLine("Artwork not found!");
                    }
                    break;
                case "3":
                    Console.Write("\nEnter title: ");
                    string? title = Console.ReadLine();
                    Console.Write("Enter artist: ");
                    string? artist = Console.ReadLine();
                    Console.Write("Enter year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Enter medium: ");
                    string? medium = Console.ReadLine();

                    AddArtwork(title, artist, year, medium);
                    break;
                case "4":
                    Console.Write("Enter title of artwork to delete: ");
                    string? deleteTitle = Console.ReadLine();
                    DeleteArtworkByTitle(deleteTitle);
                    break;
                case "5":
                    Thread.Sleep(2000);
                    InsertionSortByTitle();
                    break;
                case "6":
                    Thread.Sleep(2000);
                    InsertionSortByYear();
                    break;
                case "7":
                    SaveData(filename);
                    Console.WriteLine("Have a wonderful day. Goodbye!");
                    working = false;
                    break;
                case "8":
                    Console.WriteLine("\nNo changes were saved. Exiting the Art Collection Management System.");
                    Console.WriteLine("Hope to see you again soon!");
                    working = false;
                    break;
            }
        }
    }
}