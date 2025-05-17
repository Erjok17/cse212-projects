using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Basic priority ordering
    // Expected Result: Items dequeued in order of highest priority (5 -> 3 -> 2)
    // Defect(s) Found: Original implementation didn't properly remove dequeued items
    public void TestPriorityQueue_1()
    {
        var queue = new PriorityQueue();
        
        // Add items with different priorities
        queue.Enqueue("Bob", 2);
        queue.Enqueue("Tim", 5);
        queue.Enqueue("Sue", 3);
        
        // Verify dequeue order (highest priority first)
        Assert.AreEqual("Tim", queue.Dequeue());  // Priority 5
        Assert.AreEqual("Sue", queue.Dequeue());  // Priority 3
        Assert.AreEqual("Bob", queue.Dequeue());  // Priority 2
    }

    [TestMethod]
    // Scenario: Equal priority handling
    // Expected Result: Items with same priority dequeued in FIFO order
    // Defect(s) Found: Original didn't maintain insertion order for equal priorities
    public void TestPriorityQueue_2()
    {
        var queue = new PriorityQueue();
        
        // Add items with same priority
        queue.Enqueue("First", 1);
        queue.Enqueue("Second", 1);
        queue.Enqueue("Third", 1);
        
        // Verify FIFO order for equal priorities
        Assert.AreEqual("First", queue.Dequeue());
        Assert.AreEqual("Second", queue.Dequeue());
        Assert.AreEqual("Third", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Empty queue handling
    // Expected Result: Throws InvalidOperationException
    // Defect(s) Found: None - this verifies existing error handling
    public void TestPriorityQueue_Empty()
    {
        var queue = new PriorityQueue();
        
        try
        {
            queue.Dequeue();
            Assert.Fail("Expected exception not thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Mixed priorities with duplicates
    // Expected Result: Proper priority ordering with FIFO for duplicates
    // Defect(s) Found: Original didn't handle duplicate priorities correctly
    public void TestPriorityQueue_MixedPriorities()
    {
        var queue = new PriorityQueue();
        
        queue.Enqueue("A", 3);
        queue.Enqueue("B", 1);
        queue.Enqueue("C", 3);  // Same priority as A
        queue.Enqueue("D", 2);
        
        Assert.AreEqual("A", queue.Dequeue());  // First highest priority
        Assert.AreEqual("C", queue.Dequeue());  // Same priority, FIFO
        Assert.AreEqual("D", queue.Dequeue());  // Next priority level
        Assert.AreEqual("B", queue.Dequeue());  // Lowest priority
    }
}