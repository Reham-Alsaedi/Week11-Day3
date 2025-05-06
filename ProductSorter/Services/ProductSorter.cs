using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductSorter
    {
        private readonly string sourceFile = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "products.txt"
        );
        private readonly string outputDir = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "Sorted"
        );
        private const int BatchSize = 1000;

        public void SortProducts()
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("File 'products.txt' not found.");
                return;
            }

            Directory.CreateDirectory(outputDir);

            string byIdPath = Path.Combine(outputDir, "products_sorted_by_id.txt");
            string byNamePath = Path.Combine(outputDir, "products_sorted_by_name.txt");
            string byPricePath = Path.Combine(outputDir, "products_sorted_by_price.txt");

            var idWriter = new StreamWriter(byIdPath);
            var nameWriter = new StreamWriter(byNamePath);
            var priceWriter = new StreamWriter(byPricePath);

            var batch = new List<Product>();
            foreach (var line in File.ReadLines(sourceFile))
            {
                var parts = line.Split(',');

                if (
                    parts.Length == 3
                    && int.TryParse(parts[0], out int id)
                    && decimal.TryParse(
                        parts[2],
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal price
                    )
                )
                {
                    batch.Add(
                        new Product
                        {
                            Id = id,
                            Name = parts[1],
                            Price = price,
                        }
                    );

                    if (batch.Count >= BatchSize)
                    {
                        WriteBatch(batch, idWriter, nameWriter, priceWriter);
                        batch.Clear();
                    }
                }
            }

            if (batch.Count > 0)
            {
                WriteBatch(batch, idWriter, nameWriter, priceWriter);
            }

            idWriter.Close();
            nameWriter.Close();
            priceWriter.Close();

            Console.WriteLine("Sorted product files created successfully.");
        }

        private void WriteBatch(
            List<Product> batch,
            StreamWriter idWriter,
            StreamWriter nameWriter,
            StreamWriter priceWriter
        )
        {
            foreach (var item in batch.OrderBy(p => p.Id))
                idWriter.WriteLine($"{item.Id},{item.Name},{item.Price}");

            foreach (var item in batch.OrderBy(p => p.Name))
                nameWriter.WriteLine($"{item.Id},{item.Name},{item.Price}");

            foreach (var item in batch.OrderBy(p => p.Price))
                priceWriter.WriteLine($"{item.Id},{item.Name},{item.Price}");
        }
    }
}
