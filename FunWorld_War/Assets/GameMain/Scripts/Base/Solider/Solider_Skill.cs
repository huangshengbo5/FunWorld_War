public partial class Solider
{ 
    private Attribute Attribute;

    public void Init_Attribute()
    {
        Attribute = new Attribute();
        var npc = GameEntry.DataTable.GetDataTable<DRNPC>();
        //var npcConfig = npc.GetDataRow();
    }

    public void Add_Attribute()
    {
        //Attribute.AddAttribute();
    }
}