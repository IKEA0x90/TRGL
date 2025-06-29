import os
import json

from services import tools

class Item:
    save_dir = "/srv/TRGL/data/items"

    def __init__(self, name: str,
                 image_uri: str = '',
                 prices: dict = {},
                 price_increases: dict = {},
                 max_prices: dict = {},
                 min_prices: dict = {}
        ):
        self.name = name

        if prices:
            self.prices = prices
        else:
            self.prices = tools.Credits.ZERO_PRICES
        
        if price_increases:
            self.price_increases = price_increases
        else:
            self.price_increases = tools.Credits.ZERO_PRICES
        
        if max_prices:
            self.max_prices = max_prices
        else:
            self.max_prices = tools.Credits.ZERO_PRICES
        
        if min_prices:
            self.min_prices = min_prices
        else:
            self.min_prices = tools.Credits.ZERO_PRICES

    def to_dict(self):
        return {
            'name': self.name,
            'amount': self.amount,
            'prices': self.prices,
            'price_increases': self.price_increases,
            'max_prices': self.max_prices,
            'min_prices': self.min_prices
        }

    @staticmethod
    def from_dict(jsonDict):
        name = jsonDict.get('name', '')
        if not name:
            raise ValueError("Item name is required")
        
        prices = jsonDict.get('prices', tools.Credits.ZERO_PRICES)
        price_increases = jsonDict.get('price_increases', tools.Credits.ZERO_PRICES)
        max_prices = jsonDict.get('max_prices', tools.Credits.ZERO_PRICES)
        min_prices = jsonDict.get('min_prices', tools.Credits.ZERO_PRICES)

        return Item(
            name=name,
            prices=prices,
            price_increases=price_increases,
            max_prices=max_prices,
            min_prices=min_prices
        )
    
    def save(self):
        if not os.path.exists(Item.save_dir):
            os.makedirs(Item.save_dir)

        with open(f"{Item.save_dir}/{self.name}.json", "w") as f:
            json.dump(self.to_dict(), f)

    @classmethod
    def load(cls, name: str):
        if not os.path.exists(f"{cls.save_dir}/{name}.json"):
            raise FileNotFoundError(f"Item with name {name} does not exist.")

        with open(f"{cls.save_dir}/{name}.json", "r") as f:
            data = json.load(f)
            return cls.from_dict(data)