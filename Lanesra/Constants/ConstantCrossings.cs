using Lanesra.Models.Metros;

namespace Lanesra.Constants
{
    public static class ConstantCrossings
    {
        public static List<Crossings> crossings = new List<Crossings>();

        static ConstantCrossings()
        {
            InitializeCrossings();
        }

        private static void InitializeCrossings()
        {
            Crossings crossing = new Crossings()
            {
                metroStation = new MetroStation()
                {
                    Id = 6,
                    Name = "Paxtakor",
                    YearBuilt = 1982,
                    OrdinalNumberInItsLine = 6,
                    LineName = "Chilonzor Line"
                },
                metroStation2 = new MetroStation()
                {
                    Id = 22,
                    Name = "Alisher Navoiy",
                    YearBuilt = 1973,
                    OrdinalNumberInItsLine = 5,
                    LineName = "Oʻzbekiston Line"
                }
            };
            crossings.Add(crossing);

            Crossings crossing1 = new Crossings()
            {
                metroStation = new MetroStation()
                {
                    Id = 4,
                    Name = "Amir Temur Xiyoboni",
                    YearBuilt = 1986,
                    OrdinalNumberInItsLine = 4,
                    LineName = "Chilonzor Line"
                },
                metroStation2 = new MetroStation()
                {
                    Id = 35,
                    Name = "Yunus Rajabiy",
                    YearBuilt = 1979,
                    OrdinalNumberInItsLine = 7,
                    LineName = "Yunusobod Line"
                }
            };
            crossings.Add(crossing1);

            Crossings crossing2 = new Crossings()
            {
                metroStation = new MetroStation()
                {
                    Id = 25,
                    Name = "Oybek",
                    YearBuilt = 1972,
                    OrdinalNumberInItsLine = 8,
                    LineName = "Oʻzbekiston Line"
                },
                metroStation2 = new MetroStation()
                {
                    Id = 35,
                    Name = "Ming Orik",
                    YearBuilt = 1979,
                    OrdinalNumberInItsLine = 7,
                    LineName = "Yunusobod Line"
                }
            };
            crossings.Add(crossing2);
        }
    }
}
