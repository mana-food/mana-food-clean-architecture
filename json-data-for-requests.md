### API User
```json
[
  {
    "email": "joao@exemplo.com",
    "name": "João",
    "birthday": "1999-04-04",
    "user_type": 1, // cliente
    "cpf": "22254634097",
    "password": "joao@123"
  },
  {
    "email": "maria@exemplo.com",
    "name": "Maria",
    "birthday": "1995-08-15",
    "user_type": 0, // admin
    "cpf": "12345678901",
    "password": "maria@123"
  },
  {
    "email": "carlos@exemplo.com",
    "name": "Carlos",
    "birthday": "1988-12-22",
    "user_type": 2, // cozinha
    "cpf": "98765432100",
    "password": "carlos@123"
  },
  {
    "email": "ana@exemplo.com",
    "name": "Ana",
    "birthday": "2000-03-10",
    "user_type": 3, // operador
    "cpf": "45678912345",
    "password": "ana@123"
  },
  {
    "email": "pedro@exemplo.com",
    "name": "Pedro",
    "birthday": "1992-07-30",
    "user_type": 4, //manager
    "cpf": "32165498712",
    "password": "pedro@123"
  }
]
```
---
### API Auth

```json
{
  "email": "maria@exemplo.com",
  "password": "maria@123"
}
```
----
### API Category

```json
[
  {
    "name": "Bebidas"
  },
  {
    "name": "Lanche"
  },
  {
    "name": "Acompanhamento"
  },
  {
    "name": "Sobremesa"
  }
]
```
---
### API Item

```json
[
  {
    "name": "Água Mineral",
    "description": "Água sem gás (Lindóia)",
    "categoryId": "ID_DA_CATEGORIA"
  },
  {
    "name": "Laranja",
    "description": "Suco de larnja espremido da fruta",
    "categoryId": "ID_DA_CATEGORIA"
  },
  {
    "name": "Pão de gergelim",
    "description": "Pão de gergelim de hámgurguer",
    "categoryId": "ID_DA_CATEGORIA"
  },
  {
    "name": "Queijo Prata",
    "description": "Fatias de queijo prata",
    "categoryId": "ID_DA_CATEGORIA"
  },
  {
    "name": "Batata Frita",
    "description": "Porção de batata frita média",
    "categoryId": "ID_DA_CATEGORIA"
  }
]
```
---
### API Produto

```json
[
  {
    "name": "Água Mineral",
    "description": "Água sem gás 500ml",
    "unitPrice": 3.00,
    "categoryId": "ID_DA_CATEGORIA",
	  "itemIds": [
	    "ID_DO_ITEM"
	  ]
  },
	{
	  "name": "Suco de laranja",
	  "description": "Suco natural de laranja 300ml",
	  "categoryId": "ID_DA_CATEGORIA",
	  "unitPrice": 3.0,
	  "itemIds": [
	    "ID_DO_ITEM"
	  ]
	},
	{
	  "name": "X-burguer",
	  "description": "Lanche - Pão de gergelim com hambúrguer artesanal de patinho e quiejo prata",
	  "categoryId": "ID_DA_CATEGORIA",
	  "unitPrice": 25.50,
	  "itemIds": [
	    "ID_DO_ITEM"
	  ]
	},
	{
	  "name": "Batata Frita",
	  "description": "Porção de batata frita média",
	  "categoryId": "ID_DA_CATEGORIA",
	  "unitPrice": 12.99,
	  "itemIds": [
	    "ID_DO_ITEM"
	  ]
	}
]
```
---
### API Order

```json
{
  "paymentMethod": 1,
  "products": [
    {
      "productId": "ID_DO_PRODUTO",
      "quantity": 2
    },
    {
      "productId": "ID_DO_PRODUTO",
      "quantity": 2
    },
    {
      "productId": "ID_DO_PRODUTO",
      "quantity": 2
    }
  ]
}
```
---
