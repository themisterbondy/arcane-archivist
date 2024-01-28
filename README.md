# Magic: The Gathering Card Management API

This is a simple API for managing Magic: The Gathering cards. It provides endpoints to interact with card data, including CRUD operations for cards, as well as filtering and searching capabilities.

## Getting Started

To get started with this API, follow these steps:

1. Clone this repository to your local machine.
2. Navigate to the project directory.
3. Install the required dependencies by running `dotnet restore`.
4. Build the project using `dotnet build`.
5. Run the API using `dotnet run`.

## Endpoints

### `GET /api/cards`

- Retrieves a list of all cards.

### `GET /api/cards/{id}`

- Retrieves a specific card by its ID.

### `POST /api/cards`

- Creates a new card.

### `PUT /api/cards/{id}`

- Updates an existing card.

### `DELETE /api/cards/{id}`

- Deletes a card by its ID.

## Card Properties

- **Name**: The name of the card.
- **Type**: The type of the card (e.g., Creature, Artifact, Enchantment).
- **Rarity**: The rarity of the card (e.g., Common, Uncommon, Rare).
- **Color**: The color of the card (e.g., Red, Blue, Multi Colored).
- **Mana Cost**: The mana cost of the card.
- **Description**: A description of the card's abilities.
- **Collection**: The collection the card belongs to.
- **Edition**: The edition of the card.
- **Quantity**: The quantity of the card in stock.

## Enums

- `CardType`: Represents the types of cards.
- `CardRarity`: Represents the rarities of cards.
- `CardColor`: Represents the colors of cards.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
