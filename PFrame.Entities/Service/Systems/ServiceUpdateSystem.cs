using Unity.Entities;

namespace PFrame.Entities
{
    [UpdateInGroup(typeof(ServiceUpdateSystemGroup))]
    public class ServiceUpdateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, ref Service serviceComp, ref UnloadStageCmd unloadStageCmdComp) =>
            {
                var stageId = serviceComp.StageId;
                var stageEntity = serviceComp.StageEntity;

                if (EntityManager.HasComponent<LoadState>(entity))
                    return;
                if (EntityManager.HasComponent<UnloadState>(entity))
                    return;

                if (stageEntity != Entity.Null)
                {
                    EntityManager.AddComponent<UnloadCmd>(stageEntity);
                    EntityUtil.SetState<UnloadState, EnterUnloadStateEvent, LoadedState, ExitLoadedStateEvent>(EntityManager, entity);
                    //CommonUtil.SetState<UnloadState, UnloadEvent, LoadedState>(World, entity);
                }

                EntityManager.RemoveComponent<UnloadStageCmd>(entity);
            });

            Entities.ForEach((Entity entity, ref Service serviceComp, ref LoadStageCmd loadStageCmdComp) =>
            {
                var stageId = serviceComp.StageId;
                var stageEntity = serviceComp.StageEntity;
                var newStageId = loadStageCmdComp.StageId;

                if (stageId.Equals(newStageId))
                    return;
                if (EntityManager.HasComponent<LoadState>(entity))
                    return;
                if (EntityManager.HasComponent<UnloadState>(entity))
                    return;

                if (stageEntity != Entity.Null)
                {
                    serviceComp.NextStageId = newStageId;

                    EntityManager.AddComponent<UnloadCmd>(stageEntity);

                    EntityUtil.SetState<UnloadState, EnterUnloadStateEvent, LoadedState, ExitLoadedStateEvent>(EntityManager, entity);
                    //CommonUtil.SetState<UnloadState, UnloadEvent, LoadedState>(World, entity);
                }
                else
                {
                    //stageEntity = CreateStageEntity(stageId);

                    //EntityManager.AddComponent<LoadCmdComponent>(stageEntity);
                    serviceComp.StageId = newStageId;
                    //CommonUtil.SetState<LoadState, LoadEvent, NoneState>(World, entity);
                    EntityUtil.SetState<LoadState, EnterLoadStateEvent, NoneState, ExitNoneStateEvent>(EntityManager, entity);
                }

                EntityManager.RemoveComponent<LoadStageCmd>(entity);
            });

            Entities.ForEach((Entity entity, ref Service serviceComp, ref LoadState loadStateComp) =>
            {
                var stageId = serviceComp.StageId;
                var stageEntity = serviceComp.StageEntity;

                if (EntityManager.HasComponent<EnterLoadedStateEvent>(stageEntity))
                {
                    EntityUtil.SetState<LoadedState, EnterLoadedStateEvent, LoadState, ExitLoadStateEvent>(EntityManager, entity);
                    //CommonUtil.SetState<LoadedState, LoadedEvent, LoadState>(World, entity);
                }
            });

            Entities.ForEach((Entity entity, ref Service serviceComp, ref UnloadState unloadStateComp) =>
            {
                var stageId = serviceComp.StageId;
                var stageEntity = serviceComp.StageEntity;

                if (EntityManager.HasComponent<EnterUnloadedStateEvent>(stageEntity))
                {
                    EntityUtil.SetState<UnloadedState, EnterUnloadedStateEvent, UnloadState, ExitUnloadStateEvent>(EntityManager, entity);
                    //CommonUtil.SetState<NoneState, UnloadedEvent, UnloadState>(World, entity);

                    serviceComp.StageId = 0;// "";
                    serviceComp.StageEntity = Entity.Null;

                    //var newStageId = serviceComp.NextStageId;
                    //if (newStageId != 0)
                    //{
                    //    //stageEntity = CreateStageEntity(entity, comp1, newStageId, newStageBP, newStageBPData);

                    //    //EntityManager.AddComponent<LoadCmdComponent>(stageEntity);
                    //    serviceComp.StageId = newStageId;
                    //    //CommonUtil.SetState<LoadState, LoadEvent, NoneState>(World, entity);
                    //    EntityUtil.SetState<LoadState, EnterLoadStateEvent, NoneState>(EntityManager, entity);

                    //    serviceComp.NextStageId = 0;// CommonUtil.EmptyString32;
                    //}
                }
            });

            Entities.ForEach((Entity entity, ref Service serviceComp, ref UnloadedState unloadedStateComp) =>
            {
                var stageId = serviceComp.StageId;
                var stageEntity = serviceComp.StageEntity;

                var newStageId = serviceComp.NextStageId;
                if (newStageId != 0)
                {
                    serviceComp.StageId = newStageId;
                    //CommonUtil.SetState<LoadState, LoadEvent, NoneState>(World, entity);
                    EntityUtil.SetState<LoadState, EnterLoadStateEvent, UnloadedState, ExitUnloadedStateEvent>(EntityManager, entity);

                    serviceComp.NextStageId = 0;// CommonUtil.EmptyString32;
                }
                else
                {
                    EntityUtil.SetState<NoneState, EnterNoneStateEvent, UnloadedState, ExitUnloadedStateEvent>(EntityManager, entity);
                }
            });
        }
    }
}