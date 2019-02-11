﻿using System;
using GameCore.Activities.Components;
using GameCore.Activities.Entrant.Components;
using GameCore.Activities.Event.Components;
using GameCore.Messages;
using GameCore.Workflow;
using GameCore.Workflow.Activities;
using Victory.DataLayer.Serialization.Event;

namespace GameCore.Activities.Entrant.Graphs
{
	// Token: 0x020001A8 RID: 424
	public class EntrantPursuit : Activity
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x0001E6FC File Offset: 0x0001D6FC
		protected override WorkflowElement CreateBody()
		{
			StateGraph stateGraph = new StateGraph(string.Format("OpponentPursuit: {0}", base.Entrant.Opponent.OpponentName));
			State state = new State("initial");
			State state2 = new State("countdown");
			State state3 = new State("inpursuit");
			State state4 = new State("cooldown");
			State state5 = new State("busted");
			State state6 = new State("evaded");
			State state7 = new State("post");
			new State("aborted");
			State state8 = new State("done");
			GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitEngaged> activity = new GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitEngaged>(base.InstancedEvent, base.Entrant, Ports.Gameplay);
			GameCore.Activities.Components.CloseOnReceive<OnPursuitBusted> activity2 = new GameCore.Activities.Components.CloseOnReceive<OnPursuitBusted>(Ports.Gameplay);
			EntrantFinished activity3 = new EntrantFinished(base.InstancedEvent, base.Entrant, -8193);
			EntrantFinished activity4 = new EntrantFinished(base.InstancedEvent, base.Entrant, 8202);
			OnUpdateTick activity5 = new OnUpdateTick(base.InstancedEvent, base.Entrant);
			Conclude item = new Conclude(base.InstancedEvent, base.Entrant);
			FlushCops item2 = new FlushCops();
			LoadBehavior item3 = new LoadBehavior(base.InstancedEvent, base.Entrant, "BEHAVIOR_GAMEPLAY_PURSUIT", "PursuitMonitorBehavior");
			EntrantLoaded item4 = new EntrantLoaded(base.InstancedEvent, base.Entrant, "Entrants.Loading");
			PrepareScoringMethod item5 = new PrepareScoringMethod(base.InstancedEvent, base.Entrant);
			EnableLoadingScreen item6 = new EnableLoadingScreen(false);
			HandleTutorial item7 = new HandleTutorial(base.InstancedEvent, base.Entrant, TutorialSource.kTutorialSource_Pursuit);
			Launch item8 = new Launch(base.InstancedEvent, base.Entrant);
			AssignPursuitEscalation item9 = new AssignPursuitEscalation(base.Event.EventDef.PursuitEscalation().GetKey());
			state.EnterActivity.Activities.Add(item3);
			state.Transitions.Add(new Transition("countdown"));
			LockEntrant item10 = new LockEntrant(base.InstancedEvent, base.Entrant, true);
			EnableKeyboard item11 = new EnableKeyboard(true);
			FlushTraffic item12 = new FlushTraffic();
			LoadBlackBoards item13 = new LoadBlackBoards(base.Event, BlackBoardFlag.kBlackBoardFlag_Running);
			Countdown activity6 = new Countdown(base.InstancedEvent, base.Entrant);
			TryPerfectLaunch item14 = new TryPerfectLaunch(base.InstancedEvent, base.Entrant);
			LockEntrant item15 = new LockEntrant(base.InstancedEvent, base.Entrant, false);
			LaunchPursuitByOpponentHeat item16 = new LaunchPursuitByOpponentHeat(base.InstancedEvent, base.Entrant);
			state2.EnterActivity.Activities.Add(item10);
			state2.EnterActivity.Activities.Add(item9);
			state2.EnterActivity.Activities.Add(item5);
			state2.EnterActivity.Activities.Add(item4);
			state2.EnterActivity.Activities.Add(item11);
			state2.EnterActivity.Activities.Add(item12);
			state2.EnterActivity.Activities.Add(item6);
			state2.EnterActivity.Activities.Add(item13);
			state2.EnterActivity.Activities.Add(item7);
			state2.Transitions.Add(new Transition("inpursuit", activity6));
			state2.ExitActivity.Activities.Add(item14);
			state2.ExitActivity.Activities.Add(item8);
			state2.ExitActivity.Activities.Add(item15);
			state2.ExitActivity.Activities.Add(item16);
			VisualEffect item17 = new VisualEffect(EffectType.ENTER_COP_PURSUIT);
			EnablePointsOfInterest item18 = new EnablePointsOfInterest(4291711951u, true);
			GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitCooldown> activity7 = new GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitCooldown>(base.InstancedEvent, base.Entrant, Ports.Gameplay);
			EnablePointsOfInterest item19 = new EnablePointsOfInterest(4291711951u, false);
			state3.EnterActivity.Activities.Add(item17);
			state3.EnterActivity.Activities.Add(item18);
			state3.Transitions.Add(new Transition("cooldown", activity7));
			state3.Transitions.Add(new Transition("busted", activity2));
			state3.Transitions.Add(new Transition("done", activity4));
			state3.Transitions.Add(new Transition(activity5));
			state3.ExitActivity.Activities.Add(item19);
			EnablePointsOfInterest item20 = new EnablePointsOfInterest(3470858927u, true);
			GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitEvaded> activity8 = new GameCore.Activities.Entrant.Components.CloseOnReceive<OnPursuitEvaded>(base.InstancedEvent, base.Entrant, Ports.Gameplay);
			EnablePointsOfInterest item21 = new EnablePointsOfInterest(3470858927u, false);
			HandleTutorial item22 = new HandleTutorial(base.InstancedEvent, base.Entrant, TutorialSource.kTutorialSource_PursuitCooldown);
			state4.EnterActivity.Activities.Add(item20);
			state4.EnterActivity.Activities.Add(item22);
			state4.Transitions.Add(new Transition("inpursuit", activity));
			state4.Transitions.Add(new Transition("evaded", activity8));
			state4.Transitions.Add(new Transition("busted", activity2));
			state4.Transitions.Add(new Transition("done", activity4));
			state4.Transitions.Add(new Transition(activity5));
			state4.ExitActivity.Activities.Add(item21);
			FinishEntrant activity9 = new FinishEntrant(base.InstancedEvent, base.Entrant, FinishReason.Evaded);
			HardwareBlinkingEffectEntrant item23 = new HardwareBlinkingEffectEntrant(base.InstancedEvent, base.Entrant, 4278216447u, 3.8f, 0.4f);
			state6.EnterActivity.Activities.Add(item23);
			state6.Transitions.Add(new Transition("post", activity3));
			state6.Transitions.Add(new Transition("done", activity4));
			state6.Transitions.Add(new Transition(activity9));
			state6.ExitActivity.Activities.Add(item);
			FinishEntrant activity10 = new FinishEntrant(base.InstancedEvent, base.Entrant, FinishReason.Busted);
			HardwareBlinkingEffectEntrant item24 = new HardwareBlinkingEffectEntrant(base.InstancedEvent, base.Entrant, 4294901760u, 3f, 0.1f);
			Bust item25 = new Bust(base.InstancedEvent, base.Entrant);
			state5.EnterActivity.Activities.Add(item24);
			state5.Transitions.Add(new Transition("post", activity3));
			state5.Transitions.Add(new Transition("done", activity4));
			state5.Transitions.Add(new Transition(activity10));
			state5.ExitActivity.Activities.Add(item25);
			OverrideVehicleControl item26 = new OverrideVehicleControl(base.Entrant.Opponent.Vehicle, OverrideVehicleControl.Control.Stop);
			ReleaseBehavior item27 = new ReleaseBehavior(base.InstancedEvent, base.Entrant, "BEHAVIOR_GAMEPLAY_PURSUIT");
			PostEventScreen activity11 = new PostEventScreen(base.InstancedEvent, base.Entrant, Ports.Gameplay);
			ChangeBlackBoard item28 = new ChangeBlackBoard(BlackBoardChannel.kBlackBoard_Audio, 3114481865u);
			ChangeBlackBoard item29 = new ChangeBlackBoard(BlackBoardChannel.kBlackBoard_AI, 331198907u);
			state7.EnterActivity.Activities.Add(item28);
			state7.EnterActivity.Activities.Add(item29);
			state7.EnterActivity.Activities.Add(item26);
			state7.EnterActivity.Activities.Add(item2);
			state7.EnterActivity.Activities.Add(item12);
			state7.EnterActivity.Activities.Add(item27);
			state7.Transitions.Add(new Transition("done", activity11));
			OverrideVehicleControl item30 = new OverrideVehicleControl(base.Entrant.Opponent.Vehicle, OverrideVehicleControl.Control.Release);
			CleanupScoringMethod item31 = new CleanupScoringMethod(base.InstancedEvent, base.Entrant);
			state8.EnterActivity.Activities.Add(item30);
			state8.EnterActivity.Activities.Add(item31);
			stateGraph.InitialState = "initial";
			stateGraph.DoneState = "done";
			stateGraph.States.Add(state);
			stateGraph.States.Add(state2);
			stateGraph.States.Add(state3);
			stateGraph.States.Add(state4);
			stateGraph.States.Add(state5);
			stateGraph.States.Add(state6);
			stateGraph.States.Add(state7);
			stateGraph.States.Add(state8);
			return stateGraph;
		}
	}
}