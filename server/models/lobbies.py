from pydantic import BaseModel
from uuid import uuid4

class LobbyRules():
    def __init__(self):
        self.credits_per_stage: int = 5
        self.bonus_per_loop: int = 1

        self.bonus_credit_per_N_tomes: int = 3

        self.base_credit_increase_per_N_total_stages: int = 50
        self.instant_teleport_on_N_total_stages: int = 30

class Lobby():
    def __init__(self, host_id: str, lobby_name: str):
        self.lobby_id = uuid4().hex

        self.host_id = host_id
        self.lobby_name = lobby_name

        self.players = [host_id]
        self.rules = LobbyRules()

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