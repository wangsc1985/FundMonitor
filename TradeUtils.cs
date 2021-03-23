using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundMonitor
{
    class TradeUtils
    {
        /**
         * 印花税  卖出时千1
         * type price amount
         * @param money
         */
        public static decimal tax(int type, decimal price, int amount)
        {
            if (type == 1)
            {
                return 0.0m;
            }
            else
            {
                var money = price * amount * 100;
                return money / 1000;
            }
        }

        /**
         * 佣金  万3  最低5元
         * price amount
         */
        public static decimal commission(decimal price, int amount)
        {
            var money = price * amount * 100;
            money = money * 3 / 10000;
            if (money < 5) return 5.0m; else return money;
        }


        /**
         * 过户费   十万2
         * price amount
         */
        public static decimal transferFee(decimal price, int amount)
        {
            var money = price * amount * 100;
            return money * 2 / 100000;
        }
    }
}
