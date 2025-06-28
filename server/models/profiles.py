from pydantic import BaseModel
from typing import List, Dict
from uuid import uuid4

from services import tools

class Profile():
    def __init__(self, username: str):
        self.user_id: str = uuid4().hex
        self.username: str = username
        
        self.credits: Dict[str, int] = {
            tools.Credits.WHITE: 0,
            tools.Credits.RED: 0,
            tools.Credits.YELLOW: 0,
            tools.Credits.BLUE: 0,
            tools.Credits.ORANGE: 0,
            tools.Credits.PURPLE: 0,
            tools.Credits.BLACK: 0,
            tools.Credits.TEAL: 0
        }
        
    def save(self):
        with open(f"saves/profiles/{self.user_id}.json", "w") as f:
            f.write(self.model_dump_json())