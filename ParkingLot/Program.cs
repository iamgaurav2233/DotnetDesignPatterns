using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ParkingLot
{
    class Vehicle
    class Ticket
    {
        public string RegistrationNumber { get; set; };
        public DateTime EntryTime
        {
            get; set;
        } = DateTime.Now;
        private const int CostPerHour = 50;
        public DateTime? OutTime { get; set; } = null;
        public int CalculateCost()
        {
            TimeSpan duration = OutTime.Value - EntryTime;
            return (int)duration.TotalHours * CostPerHour;
        }
    }
    public interface IPaymentGateway
    {
        bool PayAmount(int amount);
    }

    public class StripePayment : IPaymentGateway
    {
        public bool PayAmount(int amount)
        {
            return true;
        }
    }
    public class PaypalPayment : IPaymentGateway
    {
        public bool PayAmount(int amount)
        {
            return true;
        }
    }
    interface IPaymentFactor
    {
        IPaymentGateway CreatePaymentGateway();
    }
    public class StripeFactor : IPaymentFactor
    {
        public IPaymentGateway CreatePaymentGateway()
        {
            return new StripePayment();
        }
    }
    public class PaypalFactor : IPaymentFactor
    {
        public IPaymentGateway CreatePaymentGateway()
        {
            return new PaypalPayment();
        }
    }
    enum VehicleType
    {
        car,
        bike,
        bus,
        auto,
    }
    class ParkingSpot
    {
        public VehicleType parkingType { get; set; } = 0;
        public bool IsOccupied { get; private set; } = false;
        public string? RegistrationNumber { get; private set; } = null;
        public void SetVehicle(string registrationNumber)
        {
            if (IsOccupied)
                throw new InvalidOperationException("Spot already Occupied");
            IsOccupied = true;
            RegistrationNumber = registrationNumber;
        }
        public void UnsetVehicle()
        {
            IsOccupied = false;
            RegistrationNumber = null;
        }
    }
    class ParkingLotManager
    {
        private readonly List<ParkingSpot> parkingSpots;
        public ParkingLotManager(int carSpots, int bikeSpots)
        {
            parkingSpots = Enumerable.Range(0, carSpots)
            .Select(_ => new ParkingSpot { parkingType = VehicleType.car })
            .Concat(
                Enumerable.Range(0, bikeSpots).Select(_ => new ParkingSpot { parkingType = VehicleType.bike })
            ).ToList();
        }
        public ParkingSpot? FindParking(VehicleType vehicleType)
        {

            foreach (var ps in parkingSpots)
            {
                if (ps.parkingType == vehicleType && ps.IsOccupied == false)
                {
                    return ps;
                }
            }
            return null;
        }
        public void SetParking(VehicleType vehicleType, string RegistrationNumber)
        {
            ParkingSpot? parkingSpot = FindParking(vehicleType);
            if (parkingSpot == null)
            {
                Console.WriteLine("No place exist");
            }
            parkingSpot?.SetVehicle(RegistrationNumber);
        }
        public void UnsetParking(string RegistrationNumber)
        {
            ParkingSpot? findMyVehicle = null;
            foreach (var ps in parkingSpots)
            {
                if (ps.RegistrationNumber == RegistrationNumber)
                {
                    findMyVehicle = ps;
                    break;
                }
            }
            if (findMyVehicle == null)
            {
                throw new ArgumentException(DateTime.Now + " above parking does not exist");
            }
            findMyVehicle.UnsetVehicle();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ParkingLotManager parkingLot = new ParkingLotManager(2, 3);
            parkingLot.FindParking();
        }
    }
}
