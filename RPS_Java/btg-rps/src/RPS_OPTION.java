import java.util.ArrayList;
import java.util.List;

public enum RPS_OPTION {
    ROCK {
        @Override
        public List<RPS_OPTION> GetWeakness() {
           return new ArrayList<>(List.of(PAPER, SPOCK));
        }
    },
    PAPER {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(SCISSOR, LIZARD));
        }
    },
    SCISSOR {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(ROCK, SPOCK));
        }
    },
    LIZARD {

        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(ROCK, SCISSOR));
        }

    },
    SPOCK{

        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(PAPER, LIZARD));
        }

    };
    
    public abstract List<RPS_OPTION> GetWeakness();
}
