# Ambev.DeveloperEvaluation.WebApi - Sales Endpoints

## Sales

### GET /api/Sales
- **Description:** Retrieve a list of all sales with optional pagination and search
- **Query Parameters:**
  - `PageSize` (optional, integer): Number of items per page
  - `PageIndex` (optional, integer): Page number
  - `Search` (optional, string): Filter sales by search term
- **Response:**
  ```
  [
    {
      "id": "uuid",
      "saleNumber": 123,
      "createdAt": "2025-09-29T15:00:00Z",
      "customerId": "uuid",
      "branchId": "uuid",
      "totalAmount": 1500.75,
      "cancelled": false,
      "items": [
        {
          "id": "uuid",
          "productId": "uuid",
          "quantity": 2,
          "unitPrice": 500.0,
          "discountPercent": 10.0,
          "totalPrice": 900.0
        }
      ]
    }
  ]

### POST /api/Sales

Description: Create a new sale

Request Body:

```{
  "customerId": "uuid",
  "branchId": "uuid",
  "items": [
    {
      "productId": "uuid",
      "quantity": 2,
      "unitPrice": 500.0
    }
  ]
}
**Response (201 Created)**:

```{
  "success": true,
  "message": "Sale created successfully",
  "errors": null,
  "data": {
    "id": "uuid"
  }
}

### PUT /api/Sales/{id}

Description: Update an existing sale

Path Parameters:

id (string, uuid): Sale ID

Request Body:

```{
  "id": "uuid",
  "customerId": "uuid",
  "branchId": "uuid",
  "cancelled": false,
  "items": [
    {
      "id": "uuid",
      "productId": "uuid",
      "quantity": 3,
      "unitPrice": 450.0
    }
  ]
}


Response (204 No Content): No body

### DELETE /api/Sales/{id}

Description: Delete a specific sale

Path Parameters:

id (string, uuid): Sale ID

Response (200 OK):

```{
  "success": true,
  "message": "Sale deleted successfully",
  "errors": null
}