from fastapi import FastAPI
from endpoints.lobbies import router as lobbies_router

app = FastAPI(title="TRGL")

app.include_router(lobbies_router, prefix="/lobbies")

if __name__ == "__main__":
    import uvicorn
    uvicorn.run("api:app", host="0.0.0.0", port=8745, reload=True)