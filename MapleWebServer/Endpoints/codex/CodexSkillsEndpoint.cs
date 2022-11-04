using Maple2Storage.Types.Metadata;
using MapleServer2.Data.Static;

namespace MapleWebServer.Endpoints;

public static class CodexSkillsEndpoint
{
    public static IResult Get()
    {
        List<SkillMetadata> metadata = SkillMetadataStorage.GetAllSkills().Values.ToList();
        if (metadata is null)
        {
            return Results.BadRequest();
        }

        return Results.Text(Newtonsoft.Json.JsonConvert.SerializeObject(metadata));
    }
}
