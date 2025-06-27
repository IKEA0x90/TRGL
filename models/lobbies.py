from pydantic import BaseModel
from typing import List

class Lobby(BaseModel):
    lobby_id: str
    host_id: str

    lobby_name: str
    players: List[str] = []  
    
    def save(self):
        with open(f"saves/lobbies/{self.lobby_id}.json", "w") as f:
            f.write(self.model_dump_json())

    @classmethod
    def load(cls, lobby_id: str):
        with open(f"saves/lobbies/{lobby_id}.json") as f:
            return cls.model_validate_json(f.read())