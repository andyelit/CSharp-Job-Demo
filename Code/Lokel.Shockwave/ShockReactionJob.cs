﻿/*
 * (c) Copyright 2020 Lokel Digital Pty Ltd.
 * https://www.lokeldigital.com
 * 
 * LokelPackage can be used under the Creative Commons License AU by Attribution
 * https://creativecommons.org/licenses/by/3.0/au/legalcode
 */

using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;

using Unity.Collections;


namespace Lokel.Shockwave
{
    internal struct ShockReactionJob : IJobParallelForTransform
    {
        public NativeArray<ShockwaveData> Cells;

        public void Execute(int index, TransformAccess transform)
        {
            float3 pos = transform.position;
            (_, float height, _) = Cells[index].ToParts();
            pos.y = height;
            transform.position = pos;
        }

        public static JobHandle Begin(
            NativeArray<ShockwaveData> cells,
            TransformAccessArray transforms,
            JobHandle dependency
        )
        {
            ShockReactionJob job = new ShockReactionJob()
            {
                Cells = cells
            };

            return IJobParallelForTransformExtensions.Schedule(job, transforms, dependency);
        }
    }
}
