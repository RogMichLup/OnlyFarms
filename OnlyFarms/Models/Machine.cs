using OnlyFarms.State;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Machine {

        IState state;

        public Machine()
        {
            //initial state
            this.state = new AvailableState();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000)]
        public double UtilizationCost { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [Range(1, 100)]
        public double FuelUsageRate { get; set; }

        // these methods return values should differ based on current state
        public void ChangeState(IState state)
        {
            this.state = state;
        }

        public string IsAvailableForWork()
        {
            return state.IsAvailableForWork(this);
        }

        public string FuelConsumption()
        {
            return state.FuelConsumption(this);
        }

        public string WhyUnavailable()
        {
            return state.WhyUnavailable(this);
        }
    }
}
