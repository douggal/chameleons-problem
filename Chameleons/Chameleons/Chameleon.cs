using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chameleons
{
    class Chameleon
    {
        public enum colors { Black, Brown, Gray };

        public colors Color { get; set; }

        public Chameleon(colors c)
        {
            Color = c;
        }

        public void SetColor(Chameleon c)
        {
            if (this.Color != c.Color)
            {
                switch (this.Color)
                {
                    case colors.Black:
                        if (c.Color == colors.Brown)
                        {
                            this.Color = colors.Gray;
                        } else
                        {
                            this.Color = colors.Brown;
                        }
                        break;
                    case colors.Brown:
                        if (c.Color == colors.Black)
                        {
                            this.Color = colors.Gray;
                        }
                        else
                        {
                            this.Color = colors.Black;
                        }
                        break;
                    case colors.Gray:
                        if (c.Color == colors.Black)
                        {
                            this.Color = colors.Brown;
                        }
                        else
                        {
                            this.Color = colors.Black;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
