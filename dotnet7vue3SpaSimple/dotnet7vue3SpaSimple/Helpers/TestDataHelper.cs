using dotnet7vue3SpaSimple.Models;

namespace dotnet7vue3SpaSimple.Helpers
{
    public static class TestDataHelper
    {
        private static List<ProductInfo> _productInfos;

        static TestDataHelper()
        {
            _productInfos = GenerateProductInfos();
        }

        public static List<ProductInfo> Get()
        {
            return _productInfos;
        }

        public static void Add(ProductInfo data)
        {
            data.Id = Guid.NewGuid();
            _productInfos.Add(data);
        }

        public static void Update(ProductInfo data)
        {
            foreach (var item in _productInfos.Where(x => x.Id == data.Id))
            {
                item.Name = data.Name;
                item.Calories = data.Calories;
                item.Carbs = data.Carbs;
                item.Fat = data.Fat;
                item.Protein = data.Protein;
            }
        }

        public static void Delete(Guid id)
        {
            var item = _productInfos.First(x => x.Id == id);
            _productInfos.Remove(item);
        }

        private static List<ProductInfo> GenerateProductInfos()
        {
            var temp = new List<ProductInfo>
            {
                new ProductInfo
                {
                    Name = "Frozen Yogurt",
                    Calories = 159,
                    Fat = 6.0m,
                    Carbs = 24,
                    Protein = 4.0m
                },
                new ProductInfo
                {
                    Name = "Ice cream sandwich",
                    Calories = 237,
                    Fat = 9.0m,
                    Carbs = 37,
                    Protein = 4.3m
                },
                new ProductInfo
                {
                    Name = "Eclair",
                    Calories = 262,
                    Fat = 16.0m,
                    Carbs = 23,
                    Protein = 6.0m
                },
                new ProductInfo
                {
                    Name = "Cupcake",
                    Calories = 305,
                    Fat= 3.7m,
                    Carbs = 67,
                    Protein = 4.3m
                },
                new ProductInfo
                {
                    Name = "Gingerbread",
                    Calories = 356,
                    Fat = 16.0m,
                    Carbs = 49,
                    Protein = 3.9m
                },
                new ProductInfo
                {
                    Name = "Jelly bean",
                    Carbs = 375,
                    Fat = 0.0m,
                    Calories = 94,
                    Protein = 0.0m
                },
                new ProductInfo
                {
                    Name = "Lollipop",
                    Calories = 392,
                    Fat = 0.2m,
                    Carbs = 98,
                    Protein = 0m
                },
                new ProductInfo
                {
                    Name = "Honeycomb",
                    Calories = 408,
                    Fat = 3.2m,
                    Carbs = 87,
                    Protein = 6.5m
                },
                new ProductInfo
                {
                    Name = "Donut",
                    Calories = 452,
                    Fat = 25.0m,
                    Carbs = 51,
                    Protein = 4.9m
                },
                new ProductInfo
                {
                    Name = "KitKat",
                    Calories = 518,
                    Fat = 26.0m,
                    Carbs = 65,
                    Protein = 7m
                }
            };

            foreach (var item in temp)
            {
                item.Id = Guid.NewGuid();
            }

            return temp;
        }
    }
}