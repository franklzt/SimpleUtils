using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerLoginTest {

    [Test]
    public void PlayerLoginTestSimplePasses() {

        GameObject go = new GameObject("Go");
        UITest test = go.AddComponent<UITest>();
        Assert.AreEqual(test.HelooTest(), false);

        DataBaseUI dataBaseUI = go.AddComponent<DataBaseUI>();
        Assert.AreEqual(dataBaseUI.Test(),false);
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PlayerLoginTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}