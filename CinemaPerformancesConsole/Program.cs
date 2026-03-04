using CinemaPerformances.Services;
using CinemaPerformances.UIModels;

namespace CinemaPerformances.ConsoleApp;

internal class Program
{
    private enum State
    {
        CinemaHalls,
        CinemaHallPerformances,
        Exit = 999
    }

    private readonly StorageService _storageService;
    private List<CinemaHallUIModel>? _cinemaHalls;
    
    private State _state;

    static void Main(string[] args)
    {
        new Program().Run();
    }

    private Program()
    {
        _storageService = new();
        _state = State.CinemaHalls;
    }

    public void Run()
    {
        string? command = null;
        while (_state != State.Exit)
        {
            Console.Clear();
            switch (_state)
            {
                case State.CinemaHalls:
                    CinemaHalls();
                    break;
                case State.CinemaHallPerformances:
                    CinemaHallPerformances(command ?? string.Empty);
                    break;
            }
            command = Console.ReadLine();
            _state = Update(command ?? string.Empty);
        }
    }

    private State Update(string command)
    {
        switch (_state)
        {
            case State.CinemaHalls:
                if (command == "Exit")
                    return State.Exit;
                if (!command.IsWhiteSpace())
                    return State.CinemaHallPerformances;
                break;
            case State.CinemaHallPerformances:
                if (command == "Back")
                    return State.CinemaHalls;
                break;
        }
        Console.WriteLine("Invalid command");
        return _state;
    }

    private void CinemaHalls()
    {
        LoadHalls();
        Console.WriteLine("All cinema halls:");
        foreach (var cinemaHall in _cinemaHalls ?? [])
            Console.WriteLine(cinemaHall);
        Console.WriteLine("Select the cinema hall, or type \"Exit\" to exit the program");
    }

    private void CinemaHallPerformances(string cinemaHall)
    {
        bool found = false;
        foreach (var hall in _cinemaHalls ?? [])
            if (hall.Name?.Equals(cinemaHall, StringComparison.OrdinalIgnoreCase) ?? false)
            {
                Console.WriteLine($"Performances in the hall {hall.Name}:");
                hall.LoadPerformances();
                foreach (var performance in hall.Performances)
                    Console.WriteLine(performance);
                Console.WriteLine($"Total duration: {hall.TotalDuration} minutes");
                found = true;
            }
        
        if (!found)
            Console.WriteLine($"Cinema hall {cinemaHall} doesn't exist");
        Console.WriteLine("Type \"Back\" to return to the menu");
    }

    private void LoadHalls()
    {
        if (_cinemaHalls is not null)
            return;
        _cinemaHalls = new();
        foreach (var cinemaHall in _storageService.GetAllCinemaHalls())
            _cinemaHalls.Add(new(_storageService, cinemaHall));
    }
}