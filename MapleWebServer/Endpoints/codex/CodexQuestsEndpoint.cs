using Maple2Storage.Types.Metadata;
using MapleServer2.Data.Static;

namespace MapleWebServer.Endpoints;

public static class CodexQuestsEndpoint
{
    public static IResult Get()
    {
        List<QuestMetadata> metadata = QuestMetadataStorage.GetAllQuests().Values.ToList();
        if (metadata is null)
        {
            return Results.BadRequest();
        }

        return Results.Text(Newtonsoft.Json.JsonConvert.SerializeObject(metadata));
    }
}
