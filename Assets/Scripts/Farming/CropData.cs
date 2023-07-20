using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Crop;

public class CropData
{
        public Crop cropType;
        public int cropCurrentGrowthStage;
        public int cropQuality;
        public bool cropIsWatered;
        public Vector3Int cropFarmPos;
        public Vector3 cropCellCentre;
}
