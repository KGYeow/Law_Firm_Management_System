import * as tablerIcon from "vue-tabler-icons";

export interface menu {
  header?: string;
  title?: string;
  icon?: any;
  to?: string;
  chip?: string;
  chipColor?: string;
  chipVariant?: string;
  chipIcon?: string;
  children?: menu[];
  disabled?: boolean;
  type?: string;
  subCaption?: string;
  auth?: boolean;
}

const sidebarItem: menu[] = [
  { header: "Home" },
  {
    title: "Dashboard",
    icon: tablerIcon.LayoutDashboardIcon,
    to: "/home/dashboard",
    auth: true,
  },
  {
    title: "Appointment",
    icon: tablerIcon.CalendarIcon,
    to: "/home/appointment",
  },
  {
    title: "Management",
    icon: tablerIcon.BriefcaseIcon,
    children: [
      {
        title: "Case",
        icon: tablerIcon.ScriptIcon,
        to: "/case",
      },
      {
        title: "Case History",
        icon: tablerIcon.HistoryIcon,
        to: "/case/history",
      },
      {
        title: "Task",
        icon: tablerIcon.ClipboardTextIcon,
        to: "/task",
      },
      {
        title: "Task History",
        icon: tablerIcon.HistoryIcon,
        to: "/task/history",
      },
      {
        title: "Event",
        icon: tablerIcon.CalendarIcon,
        to: "/event",
      },
    ]
  },
  { header: "Others" },
  {
    title: "Question & FAQ",
    icon: tablerIcon.QuestionMarkIcon,
    to: "/others/question",
  },
  { header: "Configuration" },
  {
    title: "User",
    icon: tablerIcon.UserCogIcon,
    to: "/configuration/user",
  },
  { header: "Sample Page" },
  {
    title: "Sample Page",
    icon: tablerIcon.ApertureIcon,
    to: "/sample-page",
    auth: true
  },
  {
    title: "Typography",
    icon: tablerIcon.TypographyIcon,
    to: "/sample-page/typography"
  },
  {
    title: "Shadow",
    icon: tablerIcon.CopyIcon,
    to: "/sample-page/shadow"
  },
  {
    title: "Icons",
    icon: tablerIcon.MoodHappyIcon,
    to: "/sample-page/icons"
  },
];

export default sidebarItem;