using System;
using UnityEngine;

namespace StackableElement
{
    /// <summary>
    /// A type of data that has a value and a stack.
    /// </summary>
    public class StackableElement<T>
    {
        public T Value { get; private set; }
        public IntWrapper Stack { get; protected set; }

        /// <summary>
        /// If a StackableElement can have multiple(more than 1) stacks.
        /// </summary>
        public BoolWrapper IsExclusive;

        /// <summary>
        /// The minimum stack a StackableElement can have. This is set to 0 if it is not specified.
        /// </summary>
        public IntWrapper MinStack { get; protected set; }

        /// <summary>
        /// The maximum stack a StackableElement can have. This is set to 999 if it is not specified.
        /// </summary>
        public IntWrapper MaxStack { get; protected set; }

        /// <summary>
        /// If a StackableElement can not change its Stack
        /// </summary>
        public BoolWrapper IsFrozen { get; protected set; }

        /// <summary>
        /// Constructor for StackableElement. Defaultly not exclusive, with a inital stack of 0, range of [0, maxStack]
        /// </summary>
        public StackableElement(T value, IntWrapper maxStack, IntWrapper minStack, IntWrapper initStack,
            BoolWrapper isExclusive)
        {
            if (maxStack.Value < minStack.Value)
            {
                throw new ArgumentException(
                    $"Max stack cannot be smaller than min stack. Max stack: {maxStack}, Min stack: {minStack}");
            }

            if (isExclusive.Value && (initStack.Value < 0 || initStack.Value > 1))
            {
                throw new ArgumentException(
                    $"The StackableElement is exclusive, but the stack is not within the correct range. Stack: {initStack}");
            }

            Value = value;
            Stack = initStack;
            IsExclusive = isExclusive;
            IsFrozen = new BoolWrapper(false);

            if (isExclusive.Value)
            {
                MinStack = new IntWrapper(0);
                MaxStack = new IntWrapper(1);
            }
            else
            {
                MinStack = minStack;
                MaxStack = maxStack;
            }
        }

        /// <summary>
        /// Add stacks to the StackableElement. 
        /// If the result exceed maximum stack, no stacks will be added.
        /// </summary>
        /// <param name="stack">The stack amount to add</param>
        public void AddStack(int stack)
        {
            if (TryAddStack(stack))
            {
                Stack.Value += stack;
            }
        }

        /// <summary>
        /// Check if the StackableElement can add more stack
        /// </summary>
        /// <param name="stack">The stack amount to add. CANNOT be negative.</param>
        /// <returns>True if StackableElement can add more stacks. 
        /// False if StackableElement cannot add more stacks.
        /// This happens if the StackableElement is frozen of the result stack is larger than the MaxStack</returns>
        public bool TryAddStack(int stack)
        {
            if (IsFrozen.Value)
            {
                return false;
            }

            if (stack < 0)
            {
                throw new ArgumentException($"Adding a negative value of stack is not allowed. Stack to add: {stack}.");
            }

            return Stack.Value + stack <= MaxStack.Value;
        }

        /// <summary>
        /// Add stacks to the StackableElement. Exceeding part will be removed.
        /// </summary>
        /// <param name="stack">The stack amount to add. CANNOT be negative</param>
        public void AddStackTrim(int stack)
        {
            if (IsFrozen.Value)
            {
                return;
            }

            if (stack < 0)
            {
                throw new ArgumentException(
                    $"Adding a negative value of stack is not allowed. OperationStack: {stack}.");
            }

            Stack.Value = (Stack.Value + stack > MaxStack.Value) ? MaxStack.Value : (Stack.Value + stack);
        }

        /// <summary>
        /// Remove stacks from the StackableElement. 
        /// If the result is below the minimum stack, no stacks will be removed.
        /// </summary>
        /// <param name="stack">The stack amount to remove</param>
        public void RemoveStack(int stack)
        {
            if (TryRemoveStack(stack))
            {
                Stack.Value -= stack;
            }
        }

        /// <summary>
        /// Check if the StackableElement can remove more stack
        /// </summary>
        /// <param name="stack">The stack amount to remove. CANNOT be negative.</param>
        /// <returns>True if StackableElement can remove more stacks. 
        /// False if StackableElement cannot remove more stacks.
        /// This happens if the StackableElement is frozen of the result stack is smaller than the MinStack</returns>
        public bool TryRemoveStack(int stack)
        {
            if (IsFrozen.Value)
            {
                return false;
            }

            if (stack < 0)
            {
                throw new ArgumentException(
                    $"Removing a negative value of stack is not allowed. OperationStack: {stack}.");
            }

            return Stack.Value - stack >= MinStack.Value;
        }

        /// <summary>
        /// emove specified number of Stacks to a StackableElement.
        /// If the result of addition ends up in having a negative, trim Stack to equal to MaxStack.
        /// </summary>
        /// <param name="stack">The number of Stack that will be removed. CANNOT be negative.</param>
        public void RemoveStackTrim(int stack)
        {
            if (IsFrozen.Value)
            {
                return;
            }

            if (stack < 0)
            {
                throw new ArgumentException(
                    $"Removing a negative value of stack is not allowed. OperationStack: {stack}.");
            }

            Stack.Value = (Stack.Value - stack < MinStack.Value) ? MinStack.Value : (Stack.Value - stack);
        }

        /// <summary>
        /// Set the property Stack to ta specified number.
        /// If the parameter stack is greater than the MaxStack, throw a StackOutOfBoundException.
        /// </summary>
        /// <param name="stack">The number of Stack that will be set to.</param>
        public void SetStack(int stack)
        {
            if (IsFrozen.Value)
            {
                return;
            }

            if (stack > MaxStack.Value || stack < MinStack.Value)
            {
                throw new ArgumentException($"Stack {stack} is not in the correct range of [{MinStack}, {MaxStack}]");
            }
            else
            {
                Stack.Value = stack;
            }
        }

        /// <summary>
        /// Set Stack to its MinStack;
        /// </summary>
        public void ResetStack()
        {
            if (IsFrozen.Value)
            {
                return;
            }

            Stack = MinStack;
        }

        /// <summary>
        /// Change the IsExclusive of a StackableElement.
        /// </summary>
        /// <param name="isExclusive">True if a Stackable can have multiple stacks. False if it can only have 0 or 1 stack.</param>
        public void SetExclusive(bool isExclusive)
        {
            IsExclusive.Value = isExclusive;
        }

        /// <summary>
        /// Change the IsFrozen of a StackableElement.
        /// </summary>
        /// <param name="isFrozen">True if the Stack of a Stackable be modified. False if it can not be modified.</param>
        public void SetFrozen(bool isFrozen)
        {
            IsFrozen.Value = isFrozen;
        }

        /// <summary>
        /// Change the MaxStack of a StackableElement. If the new MaxStack is smaller than Stack, set Stack to the new MaxStack first.
        /// </summary>
        /// <param name="maxStack">The target value of MaxStack a StackableElement would have.</param>
        public void SetMaxStack(int maxStack)
        {
            if (maxStack < MinStack.Value)
            {
                throw new ArgumentException(
                    $"Attempting to change the MaxStack from {MaxStack} to {maxStack} which is smaller than {MinStack}. "
                    + "MaxStack has to be larger than MinStack.");
            }

            if (maxStack < Stack.Value)
            {
                SetStack(maxStack);
            }

            MaxStack.Value = maxStack;
        }

        /// <summary>
        /// Change the MinStack of a StackableElement. If the new MinStack excludes Stack, set Stack to the new MinStack first.
        /// </summary>
        /// <param name="maxStack">The target value of MaxStack a StackableElement would have.</param>
        public void SetMinStack(int minStack)
        {
            if (minStack > MaxStack.Value)
            {
                throw new ArgumentException(
                    $"Attempting to change the MinStack from {MinStack} to {minStack} which is larger than {MaxStack}. "
                    + "MinStack has to be smaller than MaxStack.");
            }

            if (minStack > Stack.Value)
            {
                SetStack(minStack);
            }

            MinStack.Value = minStack;
        }

        public override string ToString()
        {
            return $"Stack: {Stack},  Value: {Value},  IsExclusive: {IsExclusive},  IsFrozen: {IsFrozen},"
                   + $"MinStack: {MinStack},  MaxStack: {MaxStack}.";
        }

        public StackableElement<E> Convert<E>() where E : T
        {
            return new StackableElement<E>((E)Value, MaxStack, MinStack, Stack, IsExclusive);
        }
    }
}