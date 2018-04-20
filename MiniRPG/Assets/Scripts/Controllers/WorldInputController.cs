using Assets.Scripts.GUI.Game;
using Assets.Scripts.Observers;
using Assets.Scripts.Utils;
using Assets.Scripts.Worlds.Items;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class WorldInputController:BaseController
    {
        public interface IPlayerClick:IObserver { void OnPlayerClick(Vector3 position); }
        public interface IPlayerPointerDown:IObserver { void OnPlayerPointerDown(Vector3 position); }
        public interface IBeginPlayerDrag:IObserver { void OnBeginPlayerDrag(Vector3 position); }

        [Obserable(typeof(IPlayerClick))]
        private event Delegates.Vector3Target OnPlayerClick = delegate{};
        [Obserable(typeof(IPlayerPointerDown))]
        private event Delegates.Vector3Target OnPlayerPointerDown = delegate{};
        [Obserable(typeof(IBeginPlayerDrag))]
        private event Delegates.Vector3Target OnBeginPlayerDrag = delegate{};

        public interface IWorldClick:IObserver { void OnWorldClick(Vector3 position, Vector3 normal); }
        public interface IWorldPointerDown:IObserver { void OnWorldPointerDown(Vector3 position, Vector3 normal); }
        public interface IBeginWorldDrag:IObserver { void OnBeginWorldDrag(Vector2 position); }
        public interface IEndWorldDrag:IObserver { void OnEndWorldDrag(Vector2 position); }
        public interface IWorldEnter:IObserver { void OnWorldEnter(Vector2 position); }
        public interface IWorldDrag:IObserver { void OnWorldDrag(Vector2 position); }
        public interface IWorldExit:IObserver { void OnWorldExit(Vector2 position); }

        [Obserable(typeof(IWorldClick))]
        private event Delegates.Vector3NormalTarget OnWorldClick = delegate{};
        [Obserable(typeof(IWorldPointerDown))]
        private event Delegates.Vector3NormalTarget OnWorldPointerDown = delegate{};
        [Obserable(typeof(IBeginWorldDrag))]
        private event Delegates.Vector2Target OnBeginWorldDrag = delegate{};
        [Obserable(typeof(IEndWorldDrag))]
        private event Delegates.Vector2Target OnEndWorldDrag = delegate{};
        [Obserable(typeof(IWorldDrag))]
        private event Delegates.Vector2Target OnWorldDrag = delegate{};
        [Obserable(typeof(IWorldEnter))]
        private event Delegates.Vector2Target OnWorldEnter= delegate{};
        [Obserable(typeof(IWorldExit))]
        private event Delegates.Vector2Target OnWorldExit= delegate{};

        public interface IItemClick:IObserver { void OnItemClick(DropItem item); }
        public interface IItemPointerDown:IObserver { void OnItemPointerDown(DropItem item); }
        public interface IBeginItemDrag:IObserver { void OnBeginItemDrag(DropItem item); }

        [Obserable(typeof(IItemClick))]
        private event Delegates.OnDropItem OnItemClick = delegate{};
        [Obserable(typeof(IItemPointerDown))]
        private event Delegates.OnDropItem OnItemPointerDown = delegate{};
        [Obserable(typeof(IBeginItemDrag))]
        private event Delegates.OnDropItem OnBeginItemDrag = delegate{};


        private WorldInput WorldInput { get; set; }
        private LayerMask TargetLayerMask { get; set; }
        private float MaxInputRange { get; set; }

        protected override IEnumerator Init()
        {
            MaxInputRange = 70;
            TargetLayerMask = LayerMask.GetMask("World", "Items", "Player");

            WorldInput = FindObjectOfType<WorldInput>();

            WorldInput.onPointerEnter += OnPointerEnter;
            WorldInput.onPointerExit += OnPointerExit;
            WorldInput.onBeginDrag += OnBeginDrag;
            WorldInput.onEndDrag += OnEndDrag;
            WorldInput.onDrag += OnDrag;
            WorldInput.onPointerClick += OnPointerClick;
            WorldInput.onPointerDown += OnPointerDown;

            yield return null;
        }

        private void OnPointerDown(Vector2 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);

            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, MaxInputRange, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 8:
                        {
                            OnWorldPointerDown.Invoke(raycastHit.point, raycastHit.normal);
                        }
                        break;
                    case 11:
                        {
                            OnItemPointerDown.Invoke(raycastHit.collider.gameObject.GetComponent<DropItem>());
                        }
                        break;
                    case 12:
                        {
                            OnPlayerPointerDown.Invoke(raycastHit.point);
                        }
                        break;
                }
            }
        }

        private void OnPointerEnter(Vector2 target)
        {
            OnWorldEnter.Invoke(target);
        }

        private void OnDrag(Vector2 target)
        {
            OnWorldDrag.Invoke(target);
        }

        private void OnEndDrag(Vector2 target)
        {
            OnEndWorldDrag.Invoke(target);
        }

        private void OnBeginDrag(Vector2 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);

            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, MaxInputRange, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 8:
                        {
                            OnBeginWorldDrag.Invoke(raycastHit.point);
                        }
                        break;
                    case 11:
                        {
                            OnBeginItemDrag.Invoke(raycastHit.collider.gameObject.GetComponent<DropItem>());
                        }
                        break;
                    case 12:
                        {
                            OnBeginPlayerDrag.Invoke(raycastHit.point);
                        }
                        break;
                }
            }
        }

        private void OnPointerExit(Vector2 target)
        {
            OnWorldExit.Invoke(target);
        }

        private void OnPointerClick(Vector2 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);
            
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, MaxInputRange, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 8:
                        {
                            OnWorldClick.Invoke(raycastHit.point, raycastHit.normal);
                        }break;
                    case 11:
                        {
                            OnItemClick.Invoke(raycastHit.collider.gameObject.GetComponent<DropItem>());
                        }
                        break;
                    case 12:
                        {
                            OnPlayerClick.Invoke(raycastHit.point);
                        }
                        break;
                }
            }
        }
    }
}
