﻿using Maple2Storage.Types.Metadata;
using MaplePacketLib2.Tools;
using MapleServer2.Constants;
using MapleServer2.Data.Static;
using MapleServer2.Servers.Game;
using MapleServer2.Types;

namespace MapleServer2.PacketHandlers.Game;

public class GlobalPortalHandler : GamePacketHandler<GlobalPortalHandler>
{
    public override RecvOp OpCode => RecvOp.GlobalPortal;

    private enum Mode : byte
    {
        Enter = 0x2
    }

    public override void Handle(GameSession session, PacketReader packet)
    {
        Mode mode = (Mode) packet.ReadByte();

        switch (mode)
        {
            case Mode.Enter:
                HandleEnter(session, packet);
                break;
            default:
                LogUnknownMode(mode);
                break;
        }
    }

    private static void HandleEnter(GameSession session, PacketReader packet)
    {
        int globalEventId = packet.ReadInt();
        int selectionIndex = packet.ReadInt();

        GlobalEvent globalEvent = GameServer.GlobalEventManager.GetEventById(globalEventId);
        if (globalEvent == null)
        {
            return;
        }

        Map map;
        switch (globalEvent.Events[selectionIndex])
        {
            case GlobalEventType.oxquiz:
                map = Map.MapleOXQuiz;
                break;
            case GlobalEventType.trap_master:
                map = Map.TrapMaster;
                break;
            case GlobalEventType.spring_beach:
                map = Map.SpringBeach;
                break;
            case GlobalEventType.crazy_runner:
                map = Map.CrazyRunners;
                break;
            case GlobalEventType.final_surviver:
                map = Map.SoleSurvivor;
                break;
            case GlobalEventType.great_escape:
                map = Map.LudibriumEscape;
                break;
            case GlobalEventType.dancedance_stop:
                map = Map.DanceDanceStop;
                break;
            case GlobalEventType.crazy_runner_shanghai:
                map = Map.ShanghaiCrazyRunners;
                break;
            case GlobalEventType.hideandseek:
                map = Map.HideAndSeek;
                break;
            case GlobalEventType.red_arena:
                map = Map.RedArena;
                break;
            case GlobalEventType.blood_mine:
                map = Map.CrimsonTearMine;
                break;
            case GlobalEventType.treasure_island:
                map = Map.TreasureIsland;
                break;
            case GlobalEventType.christmas_dancedance_stop:
                map = Map.HolidayDanceDanceStop;
                break;
            default:
                Logger.Warning("Unknown Global Event: {event}", globalEvent.Events[selectionIndex]);
                return;
        }

        session.Player.Mount = null;
        MapPortal portal = MapEntityMetadataStorage.GetPortals((int) map).FirstOrDefault(portal => portal.Id == 1);
        session.Player.Warp(map, portal?.Coord.ToFloat() ?? new(), portal?.Rotation.ToFloat() ?? new());
    }
}
