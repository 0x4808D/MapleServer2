﻿using Maple2Storage.Enums;
using MaplePacketLib2.Tools;
using MapleServer2.Constants;
using MapleServer2.Types;

namespace MapleServer2.Packets;

public static class UGCPacket
{
    private enum Mode : byte
    {
        CreateUgc = 0x02,
        SetUGCUrl = 0x04,
        ActivateBanner = 0x07,
        UpdateUGCBanner = 0x08,
        ProfilePicture = 0x0B,
        UpdateUGCItem = 0x0D,
        UpdateUGCFurnishing = 0x0E,
        UpdateUGCMount = 0x0F,
        SetEndpoint = 0x11,
        LoadBanners = 0x12,
        UpdateBanner = 0x14
    }

    public static PacketWriter SetEndpoint(string uploadEndpoint, string resourceEndpoint, string locale = "na")
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.SetEndpoint); // Function
        pWriter.WriteUnicodeString(uploadEndpoint); // Serves some random irrq.aspx
        pWriter.WriteUnicodeString(resourceEndpoint); // Serves resources
        pWriter.WriteUnicodeString(locale); // locale
        pWriter.WriteByte(2);

        return pWriter;
    }

    public static PacketWriter Unknown0()
    {
        // SO MANY CASES...
        return null;
    }

    public static PacketWriter CreateUGC(UGC ugc)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.CreateUgc);
        pWriter.Write(ugc.Type);
        pWriter.WriteLong(ugc.Uid);
        pWriter.WriteUnicodeString(ugc.Guid.ToString());

        return pWriter;
    }

    public static PacketWriter SetUGCUrl(UGC ugc)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.SetUGCUrl);
        pWriter.Write(ugc.Type);
        pWriter.WriteLong(ugc.Uid);
        pWriter.WriteUnicodeString(ugc.Url);

        return pWriter;
    }

    public static PacketWriter ActivateBanner(UGCBanner ugcBanner)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.ActivateBanner);
        pWriter.WriteActiveBannerSlot(ugcBanner);

        return pWriter;
    }

    public static PacketWriter UpdateUGCBanner(UGCBanner ugcBanner)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.UpdateUGCBanner);
        pWriter.WriteUGCBanner(ugcBanner.Id, ugcBanner.Slots);

        return pWriter;
    }

    public static PacketWriter Unknown9()
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.WriteByte(0x09);
        pWriter.WriteLong();
        pWriter.WriteInt(); // counter for loop
        for (int i = 0; i < 0; i++)
        {
            pWriter.WriteLong();
        }

        return pWriter;
    }

    public static PacketWriter SetProfilePictureUrl(int objectId, long characterId, string url)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.ProfilePicture);
        pWriter.WriteInt(objectId);
        pWriter.WriteLong(characterId);
        pWriter.WriteUnicodeString(url);

        return pWriter;
    }

    public static PacketWriter UpdateUGCItem(IFieldObject<Player> fieldPlayer, Item item)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.UpdateUGCItem);
        pWriter.WriteInt(fieldPlayer.ObjectId);
        pWriter.WriteUGCItem(item);
        pWriter.WriteClass(item.Ugc);

        return pWriter;
    }

    public static PacketWriter UpdateUGCFurnishing(IFieldObject<Player> fieldPlayer, Item item)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.UpdateUGCFurnishing);
        pWriter.WriteInt(fieldPlayer.ObjectId);
        pWriter.WriteUGCItem(item);
        pWriter.WriteClass(item.Ugc);

        return pWriter;
    }

    public static PacketWriter UpdateUGCMount(IFieldObject<Player> fieldPlayer, Item item)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.UpdateUGCMount);
        pWriter.WriteInt(fieldPlayer.ObjectId);
        pWriter.WriteUGCItem(item);
        pWriter.WriteClass(item.Ugc);

        return pWriter;
    }

    public static PacketWriter Unknown16()
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.WriteByte(0x10);
        pWriter.WriteInt();
        // sub1
        pWriter.WriteLong();
        pWriter.WriteLong();
        pWriter.WriteInt();
        pWriter.WriteUnicodeString("StrW");
        // sub2
        pWriter.WriteClass(UGC.Default);

        return pWriter;
    }

    public static PacketWriter Unknown17()
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.WriteByte(0x11);
        pWriter.WriteUnicodeString("WstrA");
        pWriter.WriteUnicodeString("WstrA");
        pWriter.WriteUnicodeString("WstrA");
        pWriter.WriteByte();

        return pWriter;
    }

    public static PacketWriter LoadUGCBanner(List<UGCBanner> banners)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.LoadBanners);

        int counter1 = 0;
        pWriter.WriteInt(counter1);
        for (int i = 0; i < counter1; i++)
        {
            bool flagA = false;
            pWriter.WriteBool(flagA);
            if (flagA)
            {
                pWriter.WriteLong();
                pWriter.WriteUnicodeString();
                pWriter.WriteByte();
                pWriter.WriteInt();
                pWriter.WriteLong();
                pWriter.WriteLong();
                pWriter.WriteUnicodeString();
                pWriter.WriteUnicodeString();
                pWriter.WriteUnicodeString();
            }
        }

        pWriter.WriteInt(banners.Count);
        foreach (UGCBanner ugcBanner in banners)
        {
            pWriter.WriteActiveBannerSlot(ugcBanner);
        }

        pWriter.WriteInt(banners.Count);
        foreach (UGCBanner ugcBanner in banners)
        {
            pWriter.WriteUGCBanner(ugcBanner.Id, ugcBanner.Slots);
        }

        return pWriter;
    }

    public static PacketWriter ReserveBannerSlots(long bannerId, List<BannerSlot> bannerSlots)
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.Write(Mode.UpdateBanner);
        pWriter.WriteLong(bannerId);
        pWriter.WriteInt(bannerSlots.Count);
        foreach (BannerSlot slot in bannerSlots)
        {
            pWriter.WriteLong(slot.Id);
            pWriter.WriteInt(1);
            pWriter.WriteLong(slot.BannerId);
            pWriter.WriteInt(slot.Date);
            pWriter.WriteInt(slot.Hour);
            pWriter.WriteLong();
        }

        return pWriter;
    }

    public static PacketWriter Unknown21()
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.WriteByte(0x15);
        pWriter.WriteInt(); // some count for loop
        for (int i = 0; i < 0; i++)
        {
            pWriter.WriteLong();
            pWriter.WriteInt();
        }

        return pWriter;
    }

    public static PacketWriter Unknown22()
    {
        PacketWriter pWriter = PacketWriter.Of(SendOp.UGC);
        pWriter.WriteByte(0x16);
        pWriter.WriteInt();

        return pWriter;
    }

    private static void WriteUGCBanner(this PacketWriter pWriter, long bannerId, List<BannerSlot> banners)
    {
        pWriter.WriteLong(bannerId);
        pWriter.WriteInt(banners.Count);
        foreach (BannerSlot bannerSlot in banners)
        {
            long bannerSlotDate = long.Parse($"{bannerSlot.Date}00000") + bannerSlot.Hour; // yes. this is stupid. Who approved this?
            pWriter.WriteLong(bannerSlotDate);
            pWriter.WriteUnicodeString(bannerSlot.UGC.CharacterName);
            pWriter.WriteBool(true); //  true = reserved, false = awaiting reservation, not sure when false is used
        }
    }

    private static void WriteActiveBannerSlot(this PacketWriter pWriter, UGCBanner ugcBanner)
    {
        pWriter.WriteLong(ugcBanner.Id);
        BannerSlot activeSlot = ugcBanner.Slots.FirstOrDefault(x => x.Active);
        pWriter.WriteBool(activeSlot is not null);
        if (activeSlot is null)
        {
            return;
        }

        pWriter.Write(UGCType.Banner);
        pWriter.WriteInt(2);
        pWriter.WriteLong(activeSlot.UGC.AccountId);
        pWriter.WriteLong(activeSlot.UGC.CharacterId);
        pWriter.WriteUnicodeString(); // unknown
        pWriter.WriteUnicodeString(activeSlot.UGC.CharacterName);
        pWriter.WriteLong(activeSlot.UGC.Uid);
        pWriter.WriteUnicodeString(activeSlot.UGC.Guid.ToString());
        pWriter.WriteByte(3);
        pWriter.WriteByte(1);
        pWriter.WriteLong(activeSlot.BannerId);
        byte loopCounter = 1; // not sure when more than 1 is used
        pWriter.WriteByte(loopCounter);
        for (byte i = 0; i < loopCounter; i++)
        {
            pWriter.WriteLong(activeSlot.Id);
            pWriter.WriteInt(2);
            pWriter.WriteLong(activeSlot.BannerId);
            pWriter.WriteInt(activeSlot.Date);
            pWriter.WriteInt(activeSlot.Hour);
            pWriter.WriteLong();
        }

        pWriter.WriteUnicodeString(activeSlot.UGC.Url);
    }

    private static void WriteUGCItem(this PacketWriter pWriter, Item item)
    {
        pWriter.WriteLong(item.Uid);
        pWriter.WriteInt(item.Id);
        pWriter.WriteInt(item.Amount);
        pWriter.WriteUnicodeString(item.Ugc.Name);
        pWriter.WriteByte(1); // unknown
        pWriter.WriteLong(item.Ugc.SalePrice);
        pWriter.WriteByte(); // unknown
    }
}
