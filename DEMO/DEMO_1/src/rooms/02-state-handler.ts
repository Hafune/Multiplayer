import { Room, Client } from "colyseus";
import { Schema, type, MapSchema } from "@colyseus/schema";

export class Player extends Schema {
    @type("int16")
    maxHp = 0;

    @type("int16")
    currentHp = 0;

    @type("number")
    x = Math.floor(Math.random() * 5) - 10;

    @type("number")
    y = 0;

    @type("number")
    z = Math.floor(Math.random() * 5) - 10;

    @type("number")
    velocityX = 0;

    @type("number")
    velocityY = 0;

    @type("number")
    velocityZ = 0;

    @type("number")
    bodyAngle = 0;

    @type("string")
    state = "";

    @type("int16")
    patchRate;
}

export class State extends Schema {
    @type({ map: Player })
    players = new MapSchema<Player>();

    playerKeys = new Set(Object.keys(new Player()));
    patchRate: number;

    constructor(patchRate: number) {
        super();
        this.patchRate = patchRate;
    }

    createPlayer(sessionId: string, data: any) {
        var player = new Player();
        player.maxHp = data.hp;
        player.currentHp = data.hp;
        player.patchRate = this.patchRate;
        this.players.set(sessionId, player);
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    movePlayer(sessionId: string, movement: any) {
        for (const key in movement) {
            if (this.playerKeys.has(key)) {
                this.players.get(sessionId)[key] = movement[key];
            }
        }
    }
}

export class StateHandlerRoom extends Room<State> {
    maxClients = 4;

    onCreate(options) {
        console.log("StateHandlerRoom created!", options);

        this.setPatchRate(20);
        this.setState(new State(this.patchRate));

        this.onMessage("move", (client, data) => {
            this.state.movePlayer(client.sessionId, data);
        });

        this.onMessage("shoot", (client, data) => {
            this.broadcast("shoot", data, { except: client });
        });
    }

    onAuth(client, options, req) {
        return true;
    }

    onJoin(client: Client, data: any) {
        client.send("hello", "world");
        this.state.createPlayer(client.sessionId, data);
    }

    onLeave(client) {
        this.state.removePlayer(client.sessionId);
    }

    onDispose() {
        console.log("Dispose StateHandlerRoom");
    }

}
