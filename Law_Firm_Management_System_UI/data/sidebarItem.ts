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
  accessName?: string;
}

const sidebarItem: menu[] = [
  { header: "Home" },
  {
    title: "Dashboard",
    icon: tablerIcon.LayoutDashboardIcon,
    to: "/dashboard-client",
    auth: true,
    accessName: "Dashboard_Client",
  },
  {
    title: "Dashboard",
    icon: tablerIcon.LayoutDashboardIcon,
    to: "/dashboard",
    auth: true,
    accessName: "Dashboard_Employee",
  },
  {
    title: "Appointments",
    icon: tablerIcon.Book2Icon,
    to: "/appointments-client",
    auth: true,
    accessName: "Appointments_Client",
  },
  {
    title: "Appointments",
    icon: tablerIcon.Book2Icon,
    to: "/appointments",
    auth: true,
    accessName: "Appointments_Employee",
  },
  {
    title: "Cases",
    icon: tablerIcon.BriefcaseIcon,
    to: "/cases",
    auth: true,
    accessName: "Cases",
  },
  {
    title: "Cases",
    icon: tablerIcon.BriefcaseIcon,
    to: "/cases-paralegal",
    auth: true,
    accessName: "Cases_Paralegal",
  },
  {
    title: "Cases",
    icon: tablerIcon.BriefcaseIcon,
    to: "/cases-client",
    auth: true,
    accessName: "Cases_Client",
  },
  {
    title: "Tasks",
    icon: tablerIcon.ClipboardTextIcon,
    to: "/tasks",
    auth: true,
    accessName: "Tasks",
  },
  {
    title: "Events",
    icon: tablerIcon.CalendarIcon,
    to: "/events-client",
    auth: true,
    accessName: "Events_Client",
  },
  {
    title: "Events",
    icon: tablerIcon.CalendarIcon,
    to: "/events",
    auth: true,
    accessName: "Events_Employee",
  },
  {
    title: "Documents",
    icon: tablerIcon.FileDescriptionIcon,
    children: [
      {
        title: "Repositories",
        icon: tablerIcon.BooksIcon,
        to: "/documents/repositories",
        auth: true,
        accessName: "Repositories",
      },
      {
        title: "Archives",
        icon: tablerIcon.ArchiveIcon,
        to: "/documents/archives",
        auth: true,
        accessName: "Archives",
      },
    ]
  },
  {
    title: "Partners",
    icon: tablerIcon.ScaleIcon,
    to: "/partners",
    auth: true,
    accessName: "Partners_Client",
  },
  {
    title: "Contacts",
    icon: tablerIcon.AddressBookIcon,
    children: [
      {
        title: "Clients",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/clients",
        auth: true,
        accessName: "Clients",
      },
      {
        title: "Paralegals",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/paralegals",
        auth: true,
        accessName: "Paralegals",
      },
      {
        title: "Partners",
        icon: tablerIcon.ScaleIcon,
        to: "/contacts/partners",
        auth: true,
        accessName: "Partners_Employee",
      },
    ]
  },
  {
    title: "Configuration",
    icon: tablerIcon.SettingsIcon,
    children: [
      {
        title: "User Settings",
        icon: tablerIcon.UserCogIcon,
        to: "/configuration/user-settings",
        auth: true,
        accessName: "UserSettings",
      },
    ]
  },
  {
    title: "Legal Information",
    icon: tablerIcon.HelpIcon,
    to: "/legal-information",
    auth: true,
    accessName: "LegalInformation",
  },
];

export default sidebarItem;