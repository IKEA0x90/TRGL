import os
import json

from pydantic import BaseModel
from typing import List
from uuid import uuid4

class LobbyRules():
    def __init__(self):
        self.credits_per_stage: int = 5
        self.bonus_per_loop: int = 1

        self.bonus_credit_per_N_tomes: int = 3

        self.base_credit_increase_per_N_total_stages: int = 50
        self.instant_teleport_on_N_total_stages: int = 30

    '''
    def to_dict(self):
        return {
            'credits_per_stage': self.credits_per_stage,
            'bonus_per_loop': self.bonus_per_loop,
            'bonus_credit_per_N_tomes': self.bonus_credit_per_N_tomes,
            'base_credit_increase_per_N_total_stages': self.base_credit_increase_per_N_total_stages,
            'instant_teleport_on_N_total_stages': self.instant_teleport_on_N_total_stages
        }
    
    @staticmethod
    def from_dict(jsonDict):
        instance = LobbyRules()

        instance.credits_per_stage = jsonDict.get('credits_per_stage', 5)
        instance.bonus_per_loop = jsonDict.get('bonus_per_loop', 1)
        instance.bonus_credit_per_N_tomes = jsonDict.get('bonus_credit_per_N_tomes', 3)
        instance.base_credit_increase_per_N_total_stages = jsonDict.get('base_credit_increase_per_N_total_stages', 50)
        instance.instant_teleport_on_N_total_stages = jsonDict.get('instant_teleport_on_N_total_stages', 30)
        
        return instance
    '''

class Lobby():
    # save_dir = "saves/lobbies"

    def __init__(self, host_id: str, lobby_name: str):
        self.lobby_id = uuid4().hex

        self.host_id = host_id
        self.lobby_name = lobby_name

        self.players = [host_id]
        self.rules = LobbyRules()

    '''
    def to_dict(self):
        return {
            'lobby_id': self.lobby_id,
            'host_id': self.host_id,
            'lobby_name': self.lobby_name,
            'players': self.players,
            'rules': self.rules.to_dict()
        }

    @staticmethod
    def from_dict(jsonDict):
        lobby_id = jsonDict.get('lobby_id', False)
        if not lobby_id:
            raise ValueError("Lobby ID not found in lobby instance.")

        host_id = jsonDict.get('host_id', False)
        if not host_id:
            raise ValueError("Host ID not found in lobby instance.")

        instance = Lobby(
            host_id=host_id,
            lobby_name=jsonDict.get('lobby_name', 'Lobby'),
        )

        instance.lobby_id = lobby_id
        instance.players = jsonDict.get('players', [host_id])

        instance.rules = LobbyRules.from_dict(jsonDict.get('rules', LobbyRules()))

        return instance
    
    def save(self):
        if not os.path.exists(Lobby.save_dir):
            os.makedirs(Lobby.save_dir)

        with open(f"{Lobby.save_dir}/{self.lobby_id}.json", "w") as f:
            json.dump(self.to_dict(), f)

    @classmethod
    def load(cls, lobby_id: str):
        if not os.path.exists(f"{cls.save_dir}/{lobby_id}.json"):
            raise FileNotFoundError(f"Lobby with ID {lobby_id} does not exist.")
        
        with open(f"{cls.save_dir}/{lobby_id}.json", "r") as f:
            data = json.load(f)
            return cls.from_dict(data)
    '''

    class CreateLobbyRequest(BaseModel):
        host_id: str
        lobby_name: str = "Lobby"

    class LobbyCreateResponse(BaseModel):
        lobby_id: str

    class LobbyJoinRequest(BaseModel):
        lobby_id: str
        player_id: str

    class LobbyLeaveRequest(BaseModel):
        lobby_id: str
        player_id: str

    class LobbyDeleteRequest(BaseModel):
        host_id: str
        lobby_id: str

    class LobbySummaryResponse(BaseModel):
        lobby_id: str
        lobby_name: str
    
    class LobbyChangeRulesRequest(BaseModel):
        host_id: str
        lobby_id: str
        rules: LobbyRules