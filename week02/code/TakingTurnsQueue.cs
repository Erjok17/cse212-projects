public class TakingTurnsQueue
{
    private readonly Queue<Person> _people = new();

    public int Length => _people.Count;

    public void AddPerson(string name, int turns)
    {
        _people.Enqueue(new Person(name, turns));
    }

    public Person GetNextPerson()
    {
        if (_people.Count == 0)
            throw new InvalidOperationException("No one in the queue.");

        Person person = _people.Dequeue();

        if (person.Turns <= 0) // Infinite turns
        {
            _people.Enqueue(person); // Always requeue
        }
        else if (person.Turns > 1) // Finite turns remaining
        {
            person.Turns--;
            _people.Enqueue(person); // Requeue with remaining turns
        }
        // Else: turns == 1 (last turn), don't requeue

        return person;
    }
}