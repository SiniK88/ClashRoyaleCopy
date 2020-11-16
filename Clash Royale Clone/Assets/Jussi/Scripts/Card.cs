using System;
using UnityEngine;

public class Card {
    public enum Effect {
        A, B, C, D
    }
    public enum State {
        IN_DECK, IN_HAND
    }

    public Effect effect;
    public State state;

    public Card(Effect effect, State state) {
        this.effect = effect;
        this.state = state;
    }


    private static EnumT GetRandomEnum<EnumT>() {
        Array values = Enum.GetValues(typeof(EnumT));
        return (EnumT)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public static Card RandomCard() {
        return new Card(
            GetRandomEnum<Effect>(),
            State.IN_HAND);
    }


}