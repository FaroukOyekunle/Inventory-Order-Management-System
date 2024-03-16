using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
﻿using Asp.NetCore_Inventory_Order_Management_System.Data;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public class NumberSequence : INumberSequence
    {
        private readonly ApplicationDbContext _context;

        public NumberSequence(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetNumberSequence(string module)
        {
            string result = "";
            try
            {
                int counter = 0;

                Models.NumberSequence numberSequence = _context.NumberSequence
                    .Where(x => x.Module.Equals(module))
                    .FirstOrDefault();
                
                if (numberSequence is null)
                {
                    numberSequence = new Models.NumberSequence();
                    numberSequence.Module = module;
                    Interlocked.Increment(ref counter);
                    numberSequence.LastNumber = counter;
                    numberSequence.NumberSequenceName = module;
                    numberSequence.Prefix = module;

                    _context.Add(numberSequence);
                    _context.SaveChanges();
                }
                else
                {
                    counter = numberSequence.LastNumber;

                    Interlocked.Increment(ref counter);
                    numberSequence.LastNumber = counter;

                    _context.Update(numberSequence);
                    _context.SaveChanges();
                }

                result = counter.ToString().PadLeft(5, '0') + "#" + numberSequence.Prefix;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
