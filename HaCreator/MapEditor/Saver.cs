using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System.Collections;
using HaCreator.MapEditor;
using HaCreator.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;
using HaRepackerLib;

namespace HaCreator.MapEditor
{
    class Saver
    {
        /// <summary>
        /// Overwrites a (mapid).img object property
        /// </summary>
        /// <param name="obj">The object that is being edited</param>
        /// <param name="boardObj">The Board object that has the edited properties</param>
        public void overWriteObj(WzSubProperty obj, LayeredItem boardObj)
        {
            if (obj != null)
            {
                ((WzStringProperty)obj["oS"]).Value = ((ObjectInfo)boardObj.BaseInfo).oS;
                ((WzStringProperty)obj["l0"]).Value = ((ObjectInfo)boardObj.BaseInfo).l0;
                ((WzStringProperty)obj["l1"]).Value = ((ObjectInfo)boardObj.BaseInfo).l1;
                ((WzStringProperty)obj["l2"]).Value = ((ObjectInfo)boardObj.BaseInfo).l2;
                ((WzCompressedIntProperty)obj["x"]).Value = boardObj.X;
                ((WzCompressedIntProperty)obj["y"]).Value = boardObj.Y;
                ((WzCompressedIntProperty)obj["z"]).Value = boardObj.Z;
                obj.ParentImage.Changed = true;
            }
        }

        /// <summary>
        /// Overwrites a (mapid).img tile property
        /// </summary>
        /// <param name="tile">The tile that is being edited</param>
        /// <param name="boardTile">The Board tile that has the edited properties</param>
        public void overWriteTile(WzSubProperty tile, LayeredItem boardTile)
        {
            //Yes, I know I could have easily made overWriteObj check for if it's a tile or obj instance
            //but this class needs more methods in it -DeathRight
            if (tile != null)
            {
                //OOOOOOH, I JUST GOT THAT, 'tS' stands for Tile Source, and 'oS' stands for Object Source
                //better put that in the Tile/ObjectInfo property summarys! -DeathRight
                ((WzStringProperty)((WzSubProperty)tile.Parent.Parent)["info"]["tS"]).Value = ((TileInfo)boardTile.BaseInfo).tS;
                ((WzStringProperty)tile["u"]).Value = ((TileInfo)boardTile.BaseInfo).u;
                ((WzCompressedIntProperty)tile["no"]).Value = int.Parse(((TileInfo)boardTile.BaseInfo).no);
                ((WzCompressedIntProperty)tile["x"]).Value = boardTile.X;
                ((WzCompressedIntProperty)tile["y"]).Value = boardTile.Y;
                //Wtf? There's a Z but it's not a property in tiles? Well, not needed I guess -DeathRight
                tile.ParentImage.Changed = true;
            }
        }

        /// <summary>
        /// Overwrites a (mapid).img foothold property
        /// </summary>
        /// <param name="foothold">The foothold that is being edited</param>
        /// <param name="boardFH">The Board foothold that has the edited properties</param>
        public void overWriteFH(WzSubProperty foothold, FootholdLine boardFH)
        {
            /*if (foothold != null)
            {
                List<String> props = new List<String>(); props.Add("next"); props.Add("prev");
                props.Add("x1"); props.Add("x2"); props.Add("y1"); props.Add("y2");

                List<String> maybeProps = new List<String>(); maybeProps.Add("force"); maybeProps.Add("forbidFallDown");

                List<Enum> propEnums = new List<Enum>(); propEnums.Add();
                if (foothold["piece"] != null && boardFH.Piece.HasValue)
                    ((WzStringProperty)foothold[""]).Value = boardFH.Piece;
            }*/
        }

        /// <summary>
        /// Adds a map object to the layer
        /// </summary>
        /// <param name="objParent">Parent of the new object</param>
        /// <param name="name">Name of the new object</param>
        /// <param name="boardObj">Board object that contains the new properties</param>
  //    /// <returns>The new object</returns>
        public /*WzSubProperty*/ void AddMapObj(WzSubProperty objParent, String name, LayeredItem boardObj)
        {
            WzSubProperty newObj = new WzSubProperty(name);
            //newObj.Parent = objParent;
            objParent.AddProperty(newObj);
            newObj.AddProperty(new WzStringProperty("oS", ((ObjectInfo)boardObj.BaseInfo).oS));
            newObj.AddProperty(new WzStringProperty("l0", ((ObjectInfo)boardObj.BaseInfo).l0));
            newObj.AddProperty(new WzStringProperty("l1", ((ObjectInfo)boardObj.BaseInfo).l1));
            newObj.AddProperty(new WzStringProperty("l2", ((ObjectInfo)boardObj.BaseInfo).l2));
            newObj.AddProperty(new WzCompressedIntProperty("x", boardObj.X));
            newObj.AddProperty(new WzCompressedIntProperty("y", boardObj.Y));
            newObj.AddProperty(new WzCompressedIntProperty("z", boardObj.Z));
            objParent.ParentImage.Changed = true;
            //return newObj;
        }

        /// <summary>
        /// Adds a map tile to the layer
        /// </summary>
        /// <param name="tileParent">Parent of the new tile</param>
        /// <param name="name">Name of the new tile</param>
        /// <param name="boardTile">Board tile that contains the new properties</param>
     // /// <returns>The new object</returns>
        public /*WzSubProperty*/ void AddMapTile(WzSubProperty tileParent, String name, LayeredItem boardTile)
        {
            WzSubProperty newObj = new WzSubProperty(name);
            //newObj.Parent = tileParent;
            tileParent.AddProperty(newObj);
            if (((WzSubProperty)tileParent.Parent)["info"]["tS"] == null)
                ((WzSubProperty)((WzSubProperty)tileParent.Parent)["info"]).AddProperty(new WzStringProperty("tS", ((TileInfo)boardTile.BaseInfo).tS));
            else
                ((WzStringProperty)((WzSubProperty)tileParent.Parent)["info"]["tS"]).Value = ((TileInfo)boardTile.BaseInfo).tS;
            newObj.AddProperty(new WzStringProperty("u", ((TileInfo)boardTile.BaseInfo).u));
            newObj.AddProperty(new WzStringProperty("no", ((TileInfo)boardTile.BaseInfo).no));
            newObj.AddProperty(new WzCompressedIntProperty("x", boardTile.X));
            newObj.AddProperty(new WzCompressedIntProperty("y", boardTile.Y));
            tileParent.ParentImage.Changed = true;
            //return newObj;
        }

        /// <summary>
        /// Overwrites the .img being edited, based on the Map open inside the Board
        /// </summary>
        /// <param name="mapBoard">Board containing everything about the map</param>
        public void WriteToMap(Board mapBoard)
        {
            #region TileObj
            foreach (LayeredItem tileObj in mapBoard.BoardItems.TileObjs)
            {
                IWzImageProperty curTileObj = mapBoard.MapInfo.mapImage[tileObj.LayerNumber.ToString()];
                #region Object
                if (tileObj is ObjectInstance)
                {
                    //i++;
                    String objName = ((ObjectInstance)tileObj).RealName;
                    IWzImageProperty curObj = curTileObj["obj"];
                    Boolean NOTWANT = false;

                    #region mapObj remover
                    foreach (IWzImageProperty mapObj in curObj.WzProperties)
                    {
                        foreach (LayeredItem newObj in mapBoard.BoardItems.TileObjs)
                        {
                            //Hope this doesn't lag alot -DeathRight
                            if (newObj is ObjectInstance)
                            {
                                if (((ObjectInstance)newObj).RealName != mapObj.Name)
                                    NOTWANT = true;
                                else
                                {
                                    NOTWANT = false;
                                    break;
                                }
                            }
                        }
                        if (NOTWANT)
                        {
                            ((WzSubProperty)curObj).RemoveProperty(mapObj);
                        }
                    }
                    #endregion
                    
                    if (curObj[objName] != null)
                    {
                        overWriteObj((WzSubProperty)curObj[objName], tileObj);
                    }
                    else
                    {
                        AddMapObj((WzSubProperty)curObj, objName, tileObj);
                    }
                }
                #endregion

                #region Tile
                else if (tileObj is TileInstance)
                {
                    //iTwo++;
                    String tileName = ((TileInstance)tileObj).RealName;
                    IWzImageProperty curTile = curTileObj["tile"];
                    Boolean NOTWANT = false;

                    #region mapTile remover
                    foreach (IWzImageProperty mapTile in curTile.WzProperties)
                    {
                        foreach (LayeredItem newTile in mapBoard.BoardItems.TileObjs)
                        {
                            //Hope this doesn't lag alot -DeathRight
                            if (newTile is TileInstance)
                            {
                                if (((TileInstance)newTile).RealName != mapTile.Name)
                                    NOTWANT = true;
                                else
                                {
                                    NOTWANT = false;
                                    break;
                                }
                            }
                        }
                        if (NOTWANT)
                        {
                            ((WzSubProperty)curTile).RemoveProperty(mapTile);
                        }
                    }
                    #endregion

                    if (curTile[tileName] != null)
                    {
                        overWriteTile((WzSubProperty)curTile[tileName], tileObj);
                        
                    }
                    else
                    {
                        AddMapTile((WzSubProperty)curTile, tileName, tileObj);
                    }
                }
                #endregion
            }
            #endregion

            #region FootHolds
            foreach (FootholdLine fh in mapBoard.BoardItems.FootholdLines)
            {
                WzImage map = mapBoard.MapInfo.mapImage;
                WzSubProperty fhDir = (WzSubProperty)map["foothold"];
                WzSubProperty fhLayer = (WzSubProperty)fhDir[((FootholdAnchor)fh.FirstDot).LayerNumber.ToString()];
                WzSubProperty fhPlat = (WzSubProperty)fhLayer[((FootholdAnchor)fh.FirstDot).PlatNumber.ToString()];
                String fhName = fh.Name;

                Boolean NOTWANT = false;

                #region foothold remover
                foreach (IWzImageProperty foothold in fhPlat.WzProperties)
                {
                    foreach (LayeredItem newFh in mapBoard.BoardItems.TileObjs)
                    {
                        //Hope this doesn't lag alot -DeathRight
                        if (newFh is TileInstance)
                        {
                            if (((TileInstance)newFh).RealName != foothold.Name)
                                NOTWANT = true;
                            else
                            {
                                NOTWANT = false;
                                break;
                            }
                        }
                    }
                    if (NOTWANT)
                    {
                        ((WzSubProperty)fhPlat).RemoveProperty(foothold);
                    }
                }
                #endregion

                if (fhPlat[fhName] != null)
                {
                    //overWriteTile((WzSubProperty)fhPlat[fhName], fh);

                }
                else
                {
                    //AddMapTile((WzSubProperty)curTile, tileName, tileObj);
                }
            }
            #endregion
        }
    }
}
