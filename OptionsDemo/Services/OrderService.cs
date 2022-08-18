using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsDemo.Services
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }

    public class OrderService : IOrderService
    {
        readonly IOptionsMonitor<OrderServiceOptions> options;

        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            this.options = options;

            this.options.OnChange(options =>
            {
                Console.WriteLine($"OnChange: {options.MaxOrderCount}");
            });
        }

        public int ShowMaxOrderCount()
        {
            return options.CurrentValue.MaxOrderCount;
        }
    }
    
    public class OrderServiceOptions
    {
        [Range(0, 10000)]
        public int MaxOrderCount { get; set; } = 100;
    }

    public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
        {
            if (options.MaxOrderCount > 10000)
            {
                return ValidateOptionsResult.Fail("MaxOrderCount cannot greater than 10000");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}
