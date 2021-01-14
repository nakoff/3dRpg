using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AnimationController
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator AnimationManagerWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }

    // [Test]
    // public void AddAnimation()
    // {
    //     var am = new Entities.AnimationController();
    //     am.AddAnimation("one", "anim_one");
    //     am.AddAnimation("two", "anim_two");

    //     Assert.IsTrue(am.GetAnimation("one") == "anim_one");
    // }

    // [Test]
    // public void AmimationChangeCallback()
    // {
    //     var curAnim = "";
    //     var am = new Entities.AnimationController();
    //     am.AddAnimation("two", "two");
    //     am.ChangeAnimation("two");

    //     Assert.IsTrue(curAnim == "two");
    // }
}
