from fastapi import APIRouter, HTTPException, Response, status, Body
from typing import Dict, List

from models import items

items_router = APIRouter()

@lobby_router.post("/", response_model=lobbies.LobbyCreateResponse, status_code=status.HTTP_201_CREATED)
def create_lobby(data: lobbies.CreateLobbyRequest):
    lobby = lobbies.Lobby(host_id=data.host_id, lobby_name=data.lobby_name)

    active_lobbies[lobby.lobby_id] = lobby

    return lobbies.LobbyCreateResponse(lobby_id=lobby.lobby_id)

@lobby_router.post("/join")
def join_lobby(data: lobbies.LobbyJoinRequest):
    lobby = active_lobbies.get(data.lobby_id)

    if not lobby:
        raise HTTPException(status_code=404, detail="Lobby not found")
    
    if data.player_id in lobby.players:
        raise HTTPException(status_code=409, detail="Already in lobby")
    
    lobby.players.append(data.player_id)
    return Response(status_code=200)

@lobby_router.post("/leave")
def leave_lobby(data: lobbies.LobbyLeaveRequest):
    lobby = active_lobbies.get(data.lobby_id)

    if not lobby:
        raise HTTPException(status_code=404, detail="Lobby not found")
    
    if data.player_id not in lobby.players:
        raise HTTPException(status_code=404, detail="Player not in lobby")
    
    lobby.players.remove(data.player_id)
    return Response(status_code=200)

@lobby_router.get("/", response_model=List[lobbies.LobbySummaryResponse])
def list_lobbies():
    return [
        lobbies.LobbySummaryResponse(
            lobby_id=lobby.lobby_id,
            lobby_name=lobby.lobby_name,
        )
        for lobby in active_lobbies.values()
    ]

@lobby_router.delete("/")
def delete_lobby(data: lobbies.LobbyDeleteRequest = Body(...)):
    lobby = active_lobbies.get(data.lobby_id)

    if not lobby:
        raise HTTPException(status_code=404, detail="Lobby not found")
    
    if lobby.host_id != data.host_id:
        raise HTTPException(status_code=403, detail="Invalid host ID")

    del active_lobbies[data.lobby_id]
    return Response(status_code=200)

@lobby_router.post("/rules")
def set_lobby_rules(data: lobbies.LobbyRulesRequest):
    lobby = active_lobbies.get(data.lobby_id)

    if not lobby:
        raise HTTPException(status_code=404, detail="Lobby not found")
    
    if data.host_id != lobby.host_id:
        raise HTTPException(status_code=403, detail="Invalid host ID")

    lobby.rules = data.rules
    return Response(status_code=200)