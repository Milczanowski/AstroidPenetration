using Assets.Scripts.Controllers.Observers;
using Assets.Scripts.GUI.Game;
using Assets.Scripts.Utils;
using Assets.Scripts.Worlds.Items;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GUIWorldInputController:BaseController
    {
        public interface IPlayerClick:IObserver { void OnPlayerClick(Vector3 position); }
        public interface IPlayerPointerDown:IObserver { void OnPlayerPointerDown(Vector3 position); }
        public interface IBeginPlayerDrag:IObserver { void OnBeginPlayerDrag(Vector3 position); }

        private event Delegates.Vector3Target OnPlayerClick = delegate{};
        private event Delegates.Vector3Target OnPlayerPointerDown = delegate{};
        private event Delegates.Vector3Target OnBeginPlayerDrag = delegate{};

        public interface IWorldClick:IObserver { void OnWorldClick(Vector3 position, Vector3 normal); }
        public interface IWorldPointerDown:IObserver { void OnWorldPointerDown(Vector3 position, Vector3 normal); }
        public interface IBeginWorldDrag:IObserver { void OnBeginWorldDrag(Vector2 position); }
        public interface IEndWorldDrag:IObserver { void OnEndWorldDrag(Vector2 position); }
        public interface IWorldEnter:IObserver { void OnWorldEnter(Vector2 position); }
        public interface IWorldDrag:IObserver { void OnWorldDrag(Vector2 position); }
        public interface IWorldExit:IObserver { void OnWorldExit(Vector2 position); }

        private event Delegates.Vector3NormalTarget OnWorldClick = delegate{};
        private event Delegates.Vector3NormalTarget OnWorldPointerDown = delegate{};
        private event Delegates.Vector2Target OnBeginWorldDrag = delegate{};
        private event Delegates.Vector2Target OnEndWorldDrag = delegate{};
        private event Delegates.Vector2Target OnWorldDrag = delegate{};
        private event Delegates.Vector2Target OnWorldEnter= delegate{};
        private event Delegates.Vector2Target OnWorldExit= delegate{};

        public interface IItemClick:IObserver { void OnItemClick(DropItem item); }
        public interface IItemPointerDown:IObserver { void OnItemPointerDown(DropItem item); }
        public interface IBeginItemDrag:IObserver { void OnBeginItemDrag(DropItem item); }

        public event Delegates.OnDropItem OnItemClick = delegate{};
        public event Delegates.OnDropItem OnItemPointerDown = delegate{};
        public event Delegates.OnDropItem OnBeginItemDrag = delegate{};

        [SerializeField]
        private WorldInput worldInput = null;
        [SerializeField]
        private LayerMask TargetLayerMask = 0;
        [SerializeField]
        private float MaxInputRange = 70;

        protected override IEnumerator Init()
        {
            worldInput = GetComponentInChildren<WorldInput>();
            worldInput.onPointerEnter += OnPointerEnter;
            worldInput.onPointerExit += OnPointerExit;
            worldInput.onBeginDrag += OnBeginDrag;
            worldInput.onEndDrag += OnEndDrag;
            worldInput.onDrag += OnDrag;
            worldInput.onPointerClick += OnPointerClick;
            worldInput.onPointerDown += OnPointerDown;

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
                            OnPlayerClick.Invoke(raycastHit.point);
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

        protected override IEnumerable InitObservers()
        {
            foreach(var observer in Observers)
            {
                if(observer is IPlayerClick)
                    OnPlayerClick += (observer as IPlayerClick).OnPlayerClick;

                if(observer is IPlayerPointerDown)
                    OnPlayerPointerDown += (observer as IPlayerPointerDown).OnPlayerPointerDown;

                if(observer is IBeginPlayerDrag)
                    OnBeginPlayerDrag += (observer as IBeginPlayerDrag).OnBeginPlayerDrag;

                if(observer is IWorldClick)
                    OnWorldClick += (observer as IWorldClick).OnWorldClick;

                if(observer is IWorldPointerDown)
                    OnWorldPointerDown += (observer as IWorldPointerDown).OnWorldPointerDown;

                if(observer is IBeginWorldDrag)
                    OnBeginWorldDrag += (observer as IBeginWorldDrag).OnBeginWorldDrag;

                if(observer is IEndWorldDrag)
                    OnEndWorldDrag += (observer as IEndWorldDrag).OnEndWorldDrag;

                if(observer is IWorldDrag)
                    OnWorldDrag += (observer as IWorldDrag).OnWorldDrag;

                if(observer is IWorldExit)
                    OnWorldExit += (observer as IWorldExit).OnWorldExit;

                if(observer is IItemClick)
                    OnItemClick += (observer as IItemClick).OnItemClick;

                if(observer is IItemPointerDown)
                    OnItemPointerDown += (observer as IItemPointerDown).OnItemPointerDown;

                if(observer is IBeginItemDrag)
                    OnBeginItemDrag += (observer as IBeginItemDrag).OnBeginItemDrag;


                yield return null;
            }

            Observers.Clear();
        }
    }
}
