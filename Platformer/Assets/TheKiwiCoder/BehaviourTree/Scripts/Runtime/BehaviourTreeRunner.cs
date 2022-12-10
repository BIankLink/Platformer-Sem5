using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class BehaviourTreeRunner : LivingEntity {

        // The main behaviour tree asset
        public BehaviourTree tree;
        public GameObject[] wayPoints;
        // Storage container object to hold game object subsystems
        Context context;
        [SerializeField]MeshFilter meshFilter;
        // Start is called before the first frame update
        private void Awake()
        {
            OnDeath += onDeath;
        }
        protected override void Start() {
            base.Start();
            context = CreateBehaviourTreeContext();
            tree = tree.Clone();
            tree.Bind(context);
            
            //tree.blackboard.itself = this.gameObject;
        }

        // Update is called once per frame
        void Update() {
            if (tree) {
                tree.Update();
                
            }
        }

        Context CreateBehaviourTreeContext() {
            return Context.CreateFromGameObject(gameObject);
        }

        private void OnDrawGizmosSelected() {
            if (!tree) {
                return;
            }

            BehaviourTree.Traverse(tree.rootNode, (n) => {
                if (n.drawGizmos) {
                    n.OnDrawGizmos();
                }
            });
        }
        void onDeath()
        {
            if (GameManager.instance.firstDestroyedGrave)
            {
                return;
            }
            GameManager.instance.firstDestroyedGrave=true;
        }
    }
}