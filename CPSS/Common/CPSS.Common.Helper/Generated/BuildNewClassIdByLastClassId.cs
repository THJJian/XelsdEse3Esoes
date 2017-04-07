namespace CPSS.Common.Core.Helper.Generated
{
    public class BuildNewClassIdByLastClassId
    {
        /// <summary>
        /// 根据最后一次生成的classid生成新的classid
        /// </summary>
        /// <param name="lastClassId"></param>
        /// <returns></returns>
        public static string GeneratedNewClassIdByLastClassId(string lastClassId)
        {
            var last6PlaceChar = lastClassId.Substring(lastClassId.Length - 6, 6);
            var beforPartPlaceChar = lastClassId.Substring(0, lastClassId.Length - 6);
            var newClassIdChar = (int.Parse(last6PlaceChar) + 1).ToString();
            for (int i = newClassIdChar.Length; i < 6; i++)
            {
                newClassIdChar = string.Concat("0", newClassIdChar);
            }
            return string.Concat(beforPartPlaceChar, newClassIdChar);
        }
    }
}