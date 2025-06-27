from fastapi import APIRouter
from uuid import uuid4

router = APIRouter()

lobbies = {}

@router.post("/", response_model=LobbyResponse)
def create_lobby(data: LobbyCreateRequest):
    lobby_id = str(uuid4())
    lobbies[lobby_id] = {
        "lobby_id": lobby_id,
        "name": data.name,
        "host_custom_id": data.host_custom_id,
        "players": [],
        "status": "open"
    }
    return lobbies[lobby_id]

@router.get("/")
def list_lobbies():
    return list(lobbies.values())

@router.get("/{lobby_id}")
def get_lobby(lobby_id: str):
    lobby = lobbies.get(lobby_id)
    if not lobby:
        return {"error": "Lobby not found"}
    return lobby

@router.delete("/{lobby_id}")
def delete_lobby(lobby_id: str):
    if lobby_id in lobbies:
        del lobbies[lobby_id]
        return {"status": "deleted"}
    return {"error": "Lobby not found"}
