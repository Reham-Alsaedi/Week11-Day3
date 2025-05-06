***ProductSorter API***

This is a simple ASP.NET Core Web API project that reads product data from a text file (products.txt), sorts the products by ID, Name, or Price, and stores the sorted results in separate files. The API then serves the sorted and paginated data through a GET endpoint.

**Features**

- Sort products by Id, Name, or Price
- Paginate sorted results
- API endpoint: GET /api/Products/GetProducts
- Uses plain text files for data storage
- No database required

ProductSorter/
   │
   ├── Controllers/
   │   └── ProductsController.cs     # API controller to return sorted products
   │
   ├── Models/
   │   └── Product.cs                # Product model
   │
   ├── Services/
   │   └── ProductSorter.cs         # Logic to sort and write product data
   │
   ├── Data/
   │   ├── products.txt             # Source file with raw product data
   │   └── Sorted/                  # Output directory with sorted files
   │       ├── products_sorted_by_id.txt
   │       ├── products_sorted_by_name.txt
   │       └── products_sorted_by_price.txt
   │
   ├── Program.cs                   # Main entry point to run sorting and start app
   └── README.md                    # Project instructions (this file)
